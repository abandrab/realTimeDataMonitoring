const SPO2_WAVEFORM = 0xf8;
const SPO2_O2_V = 0xf9;
const SPO2_PULSE_V = 0xfa;
const SPO2_INFO = 0xfb;
const SPO2_Q_P_IDX = 0xfc;

process.on('message', function(datas) {
	var input = JSON.parse(datas).data;
	var i = 4;
    var pack = {
        SO:0,
        SP:0,
        SW:[],
        SI:0,
		notification: false
    };
    while (i<input.length)
    {
		if (input[i] == SPO2_WAVEFORM)
        {
			++i;
            while(input[i] < 240)
            {
                pack.SW.push(input[i]);
                i++;
            }
        }               
        switch(input[i])
        {
            case SPO2_O2_V:
				++i;
				console.log("SpO2: " + input[i]);
				pack.SO = input[i];
				if(pack.SO<95){
					pack.notification = true;
				}
                break;
			case SPO2_PULSE_V:
                ++i;
                console.log("Puls: " + input[i]);
                pack.SP = input[i];
				if(pack.SP < 60 || pack.SP>100){
					pack.notification = true;
				}
                break;
            case SPO2_INFO:
                ++i;
                pack.SI = input[i];
                switch(input[i])
                {
                    case 1:
                        console.log("No sensor connected!");
                        break;
                    case 2:
						console.log("No finger to probe!");
                        break;
                    case 3:
						console.log("Low fusion!");
                        break;
                    case 0:
                        console.log("OK!");
                        break;
					case 69:
						console.log("Module error!");
						break;
				}
				break;
		}
        ++i;
	}
    process.send(JSON.stringify(pack));
});

