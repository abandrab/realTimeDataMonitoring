const ECG_WAVEFORM = 0xf8;
const ECG_PULSE_V = 0xf9;
const ECG_STATUS = 0xfc;

process.on("message", function(datas){
	var input = JSON.parse(datas).data;
	var i = 4;
	var pack = {
		EP: 0,
		ES: [],
		ECG1: [],
		ECG2: [],
		ECG3: [],
		notification: false
	}
	console.log(datas);
	while(i<input.length)
	{
		if(input[i] == ECG_WAVEFORM){
			++i;
			var pair = {ECG1: 0, ECG2:0, ECG3:0};
			pack.ECG1.push(input[++i]);
			pack.ECG2.push(input[++i]);
			pack.ECG3.push(input[++i]);
		}
		if(input[i] == ECG_PULSE_V){
			pack.EP = input[++i];
			if(pack.EP<60 || pack.EP>100){
				pack.notification = true;
			}
		}
		if(input[i] == ECG_STATUS){
			count=0;
			while(count<4){
				pack.ES.push(input[++i]);
				switch(input[i]){
					case 0: console.log('ECG OK!'); 
						break;
					case 1: console.log('ECG Pacemaker detected!'); break;
					case 4: console.log('ECG Initialization!'); break;
					case 5: console.log('ECG Search electrodes!'); break;
					case 8: console.log('ECG Simulated output!'); break;
					case 10: console.log('ECG Module error!'); break;
				}
				count ++;
			}
		}
		++i;
	}
	process.send(JSON.stringify(pack));
});
