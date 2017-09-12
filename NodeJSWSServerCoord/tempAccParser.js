const TEMP_V = 0xf9;
const ACC_V = 0xfa;

process.on("message", function(datas){
    var i = 4;
    pack = {
            TC: NaN,
			X: [],
			Y: [],
			Z: [],
			notification: false
    } 
	var input = JSON.parse(datas).data;
    while(i<input.length){
        if(input[i] == TEMP_V){
            var val1 = input[++i];
            var val2 = input[++i];
            pack.TC = (val1*10 + val2)/10;
			if(pack.TC<36.5 || pack.TC>37){
				pack.notification = true;
			}
        }
		if(input[i] == ACC_V){
			pack.X.push(input[++i]);
			pack.Y.push(input[++i]);
			pack.Z.push(input[++i]);
		}
		i++;
    }
    process.send(JSON.stringify(pack));
});
