const readline = require("readline-sync");
let count = 0;
let check;
while(true)
{
    check = Number(readline.question("Do you wish to continue : "));
    if(check == 1)
    {
        count++;
    }
    else
    {
        break;
    }
}
console.log(count);