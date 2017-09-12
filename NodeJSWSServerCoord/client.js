function Client(email, token, socket, id) {
    this.email = email;
	this.token = token;
    this.socket = socket;
	this.id = id;
}
module.exports = Client;
