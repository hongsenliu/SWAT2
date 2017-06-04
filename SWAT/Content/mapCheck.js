if (typeof (document.getElementByIdOrName) == "undefined") {
    document.getElementByIdOrName = function (idToFind) {
        // first try getElementById
        var foundElements = document.getElementById(idToFind);
        if (foundElements && foundElements.id == idToFind) {
            return foundElements;
        }

        // no element found so far, check the name+[] (for multicheckbox type entries)
        foundElements = document.getElementsByName(idToFind + "[]");
        if (typeof (foundElements[0]) != "undefined") {
            return foundElements[0];
        }

        // still nothing, just use the name
        foundElements = document.getElementsByName(idToFind);
        if (typeof (foundElements[0]) != "undefined") {
            return foundElements[0];
        }

        return null;
    }
}