import { AzureFunction, Context, HttpRequest } from "@azure/functions"
/* import * as fs from 'fs';
import * as rd from 'readline'; */
import badwords from "./array";

function containsBadWord(text: String) {
    // https://github.com/RobertJGabriel/Google-profanity-words
    // Ty corporate overlord <3

    /* const readStream = fs.createReadStream("badwords");
    var reader = rd.createInterface(readStream);
    reader.on("line", (word: string) => {
        if(text.includes(word)) return true;
    }); */

    badwords.forEach(word => {
        if(text.includes(word)) return true;
    });

    return false;
}

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');
    const content = (req.query.content || req.body);

    console.log("Received data:");
    console.log(content);

    let response :String;

    if(content) {
        if(containsBadWord(content)) {
            response = "bad";
        } else {
            response = "good";
        }
    }

    context.res = {
        // 200 is cool
        status: 200,
        body: response
    };
};

export default httpTrigger;