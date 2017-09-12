const Client = require('./client')
const inherits = require('util').inherits;

class Doctor{
     constructor(email, token, socket, id, patientId) {
        Client.call(this, email, token, socket, id);
        this.patientId = patientId;
    }
}
inherits(Doctor, Client);
module.exports = Doctor;
