const db = require('./dbAccess'); 
var buffer = [];

process.on("message", function(dataPack){
	process.send('');	
	if(dataPack!==''){
	    	if(buffer.length <= 9000){
	            buffer.push(dataPack);
 	     	}
       		else{
                db.save(buffer);
                buffer.length = 0;
				buffer.push(dataPack);
			}
	}
});

