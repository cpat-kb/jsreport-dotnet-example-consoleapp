function emptyValue(value) {
    if (value === '')
        return '-';
    else
        return value;
}

function millisecondsToTime(milli) {
    if (!milli)
        return "-";
    var numMilli = 0;
    if (milli.toString() == "N/A") {
        return "—";
    }
    else {
        numMilli = Number(milli);
    }
    if (numMilli == 0) {
        return "—";
    }
    var ms = numMilli % 1000;
    numMilli = (numMilli - ms) / 1000;
    var secs = numMilli % 60;
    numMilli = (numMilli - secs) / 60;
    var mins = numMilli % 60;
    var hrs = (numMilli - mins) / 60;
    var returnFormattedTime = "";
    if (hrs > 0) {
        returnFormattedTime = (hrs.toString() + "h " + mins.toString() + "m " + secs.toString() + "s");
    }
    else if (mins > 0) {
        returnFormattedTime = (mins.toString() + "m " + secs.toString() + "s");
    }
    else {
        returnFormattedTime = (secs.toString() + "s");
    }
    return returnFormattedTime;
}

function displayScore(score) {
    var display = "-";
    if (score) {
        if (score > 0) {
            if (score <= 1) {
                display = ((score * 100).toFixed(2).toString() + "%");
            }
            else if (score > 1 && score <= 100) {
                display = (score.toFixed(2).toString() + "%");
            }
            else if (score > 100) {
                display = score.toString();
            }
        }
    }
    if (display.indexOf(".00") > -1) {
        display = display.replace(".00", "");
    }
    return display;
}

function casualDate(numberDate) {
    if (numberDate) {
        return this.dateFormatter.casualDate(numberDate);
    }
    return "-";
}