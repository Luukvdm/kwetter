import { AzureFunction, Context, HttpRequest } from "@azure/functions"
/* import * as fs from 'fs';
import * as rd from 'readline'; */
import badwords from "./array";

function containsBadWord(context: Context, text: String) {
    // https://github.com/RobertJGabriel/Google-profanity-words
    // Ty corporate overlord <3

    /* const readStream = fs.createReadStream("badwords");
    var reader = rd.createInterface(readStream);
    reader.on("line", (word: string) => {
        if(text.includes(word)) return true;
    }); */
    let result = false;

    badwords.every(word => {
        if(text.indexOf(word) >= 0) {
            result = true;
            return false;
        }
        return true;
    });

    return result;
}

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');
    const content = (req.query.content || req.body);

    context.log("Received data:");
    context.log(content);

    let response :String;

    if(content) {
        if(containsBadWord(context, content)) {
            response = "bad";
            context.log("Word is bad :(");
        } else {
            response = "good";
            context.log("Word is good :)");
        }
        context.res = {
            // 200 is cool
            status: 200,
            body: response
        };
    } else {
        context.log("Content bad")
        context.res = {
            status: 400,
            body: "dunno"
        };
    }
};

export default httpTrigger;