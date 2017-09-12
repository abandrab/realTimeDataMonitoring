const serialport = require('serialport');
const portName = '/dev/ttyUSB0';
const cp = require('child_process');
const moment = require("moment-timezone");

//Parsers
const spo2Parser = cp.fork('./spo2Parser');
const ecgParser = cp.fork('./ecgParser');
const tempAccParser = cp.fork('./tempAccParser');

//Serial packet delimiters
const SOP = 0xff;
const EOP = 0xfd;

//Node types
const ECG_NODE = 0
const SPO2_NODE = 1
const TEMP_ACC_NODE = 3

serialport.list(function (err, ports) {
  ports.forEach(function(port) {
    console.log('Com-name: '+port.comName);
    console.log('pnpId: ' + port.pnpId);
    console.log('manufacturer: ' + port.manufacturer);
  });
});

var sp = new serialport(portName, {
    baudRate: 115200,
    dataBits: 8,
    parity: 'none',
    stopBits: 1,
    flowControl: false,
    parser: serialport.parsers.raw
});


sp.on('data', function(input) {
	parse(input);
	//iterate(input);
});

var dataPack = {
	sensorId: 0,
	nodeType: '',
	patientId: '',
	packNumber: 0,
	package: {},
	timestamp: null
};

var iterate = function(input){
	for(var i = 0; i<input.length; i++)
	{
		console.log(i + ': '+ input[i]);
	}

}
var parse = function(input){
	var value;
	var i = 0;
	value = input[0];
	if(value == SOP)
	{
		dataPack.sensorId = input[++i];
		var nodeType = input[++i];
		dataPack.packNumber = input[++i];
		dataPack.timestamp = moment().tz("Europe/Bucharest").format();
		
		var datas =JSON.stringify(input);
		if(nodeType == SPO2_NODE)
		{	
			dataPack.nodeType = 'SpO2';
			console.log('SpO2 Node!');
			spo2Parser.send(datas);
		}
		if(nodeType == ECG_NODE)
		{
			dataPack.nodeType = 'ECG';
			console.log('ECG Node!');
            ecgParser.send(datas);
		}
		if(nodeType == TEMP_ACC_NODE)
		{
			dataPack.nodeType = 'TAC';
			console.log('Temperature/Acceleration Node!');
			tempAccParser.send(datas);
		}
	}
};			

spo2Parser.on('message', function(pack){
	dataPack.package = JSON.parse(pack);
	console.log('SpO2 Parsed Package: ');
	console.log(dataPack);
	process.send(dataPack);	
});
ecgParser.on('message', function(pack){
	dataPack.package = JSON.parse(pack);
	console.log("ECG Parsed Package: ");
	console.log(dataPack);
	process.send(dataPack);
});	       
tempAccParser.on('message', function(pack) {
	dataPack.package = JSON.parse(pack);
	console.log("Temperature + Acceleration Parsed Package: ");
	console.log(dataPack);
	process.send(dataPack);
});

sp.on('open', function(){
	console.log('Opened!');
});
sp.on('error', function(message) {
	console.log('error: ' + message);
});
