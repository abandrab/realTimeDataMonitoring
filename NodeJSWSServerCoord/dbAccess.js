"use strict"

const httpClient = require('node-rest-client').Client;
const path = 'http://192.168.43.33:82/backend/api';

var exports = module.exports = {};
exports.getPatient = function(token, callback) {
 	var client = new httpClient();
	var args = {
		headers: { 
			"Authorization": "Bearer " + token
		}
	};
	var SensorIds = [];
	client.get(path + '/patient/getpatient', args, function (data, response) {
		console.log(data);
		callback(data);
	});
};

exports.getDoctor = function(token, callback) {
        var client = new httpClient();
        var args = {
                headers: {
                        "Authorization": "Bearer " + token
                }
        };
        client.get(path + '/doctor/getdoctor', args, function (data, response){
                console.log(data);
                callback(data);
        });
};


exports.save = function(dataBuffer){
	var client= new httpClient();
        var args = {
                data: dataBuffer,
                headers: { 
                        //"Authorization": "Bearer " + token,
                        "Content-Type": "application/json" 
                }
        };
        client.post(path + '/data/addmany', args, function (response){
                console.log(response);
        });
};
