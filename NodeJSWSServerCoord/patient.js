const Client = require('./client')
const inherits = require('util').inherits;

function Patient(email, token, socket, id, sensors, doctorId, assistantId) {
   	Client.call(this, email, token, socket, id);
	this.sensorIds = sensors;
    this.doctorId = doctorId;
    this.assistantId = assistantId;
}
inherits(Patient, Client);
module.exports = Patient;

