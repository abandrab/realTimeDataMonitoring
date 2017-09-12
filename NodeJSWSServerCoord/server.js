const WebSocketServer = require("ws").Server
const jwt = require('jsonwebtoken');
const PORT = process.env.PORT || 5000
const cp = require('child_process');

const interceptor = cp.fork('./interceptor');
const auth= require('./authConfig');
const Patient =  require('./patient');
const Doctor = require('./doctor');
const db = require('./dbAccess');
const buffer = cp.fork('./dbBuffer');

const options = { 
    audience: auth.AUDIENCE,
    issuer: auth.ISSUER, 
    algorithms: auth.ALG
};

var id;
var token;
var role;
var email;
var doctors = [];

buffer.on('message', function(msg){
	buffer.send('');
});
buffer.send('');


const wss = new WebSocketServer({ 
    verifyClient: function (info, cb) {
		token = info.req.headers['sec-websocket-protocol'];	 
		console.log(token);
		if (!token)
			 cb(false, 401, 'Unauthorized')
		else {
			 jwt.verify(token, new Buffer(auth.SECRET_KEY, 'base64'), options , function (err, decoded) {
				 if (err) {
					console.log('Unauthorized client! ' + err)
					cb(false, 401, 'Unauthorized')
				 } else {
					console.log('Authorized! '+ JSON.stringify(decoded));
					email = decoded['email'];
					role = decoded['role'];
					cb(true)
				  }
			  })
		}
    },
    port: PORT
});

console.log("websocket server created");
wss.on("connection", function(wsocket) {
	console.log("websocket connection open");
	var patient = null;
	if(role === 'Patient')
	{
		db.getPatient(token, function(data) {
        		patient = new Patient(email, token, wsocket, data.Id,  data.SensorIds, data.DoctorId, data.AssistantId);
        		interceptor.on('message', function(record) {
					if(patient != null && patient.sensorIds.includes(record.sensorId)){
                			record.patientId = patient.id;
						try{
                			patient.socket.send(JSON.stringify(record));
                			doctors.forEach(function(doctor){
								if(record.package.notification && doctor.id === patient.doctorId && doctor.patientId === null){
										doctor.socket.send(JSON.stringify(record));
								}
								if(doctor.id === patient.doctorId && doctor.patientId === patient.id){
										doctor.socket.send(JSON.stringify(record));
								}	
							});
							buffer.send(record);
						}
						catch(e){
							console.log('channel closed! '+e);
						}
           	 		}
        		});
		});
    }
	else if (role === 'Doctor' || role === 'Assistant'){
		var patientId = null;
		wsocket.on("message", function(msg){
			patientId = msg;
		});
		db.getDoctor(token, function(data){
			doctors.push(new Doctor(email, token,  wsocket, data, patientId));
		});
	}
	wsocket.on("close", function() {
		if(patient != null){
			patient = null;
			console.log('Patient left!');
		} 
		doctors.forEach(function(doctor){
			if(doctor.socket === wsocket){
				doctors.splice(doctors.indexOf(doctor), 1);
				console.log('Doctor deleted!');
			}
		});
		console.log("websocket connection close");
    });
});
