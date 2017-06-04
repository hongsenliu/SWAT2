window.onload = function () {
    pplPerHH();
};

function pplPerHH()
{
    var population = document.getElementById('Population').value;
    var households = document.getElementById('numHouseholds').value;

    if (population != null && population.trim() != "" && households != null && households.trim() != "" && households > 0 && population > 0)
    {	
        document.getElementById('PeoplePerHH').disabled = true;
        document.getElementById('PeoplePerHH').value = population / households;
    }
    else
    {
        document.getElementById('PeoplePerHH').value = '';
        document.getElementById('PeoplePerHH').disabled = false;
    }
}

function chkAreaPercent()
{
    var forest = parseFloat(document.getElementById('admintblCommunityareaForest').value);
    var agc = parseFloat(document.getElementById('admintblCommunityareaAgC').value);
    var agh = parseFloat(document.getElementById('admintblCommunityareaAgH').value);
    var paved = parseFloat(document.getElementById('admintblCommunityareaPaved').value);
    var areainf = parseFloat(document.getElementById('admintblCommunityareaInf').value);
    var sw = parseFloat(document.getElementById('admintblCommunityareaSW').value);
    var wet = parseFloat(document.getElementById('admintblCommunityareaWet').value);
    var nat = parseFloat(document.getElementById('admintblCommunityareaNat').value);
    var prot = parseFloat(document.getElementById('admintblCommunityareaProt').value);
    var bestm = parseFloat(document.getElementById('admintblCommunityareaBM').value);
    if(prot < 0 || prot > 100)
    {
        document.getElementById('admintblCommunityareaProt').style.backgroundColor="red";
        alert("Percentage of land under protected has to be >= 0 and <= 100.");
    }
    else if(nat < 0 || nat > 100)
    {
        document.getElementById('admintblCommunityareaNat').style.backgroundColor="red";
        alert("Percentage of native or natural lands has to be >= 0 and <= 100.");
    }
    else if(wet < 0 || wet > 100)
    {
        document.getElementById('admintblCommunityareaWet').style.backgroundColor="red";
        alert("Percentage of wetlands has to be >= 0 and <= 100.");
    }
    else if(sw < 0 || sw > 100)
    {
        document.getElementById('admintblCommunityareaSW').style.backgroundColor="red";
        alert("Percentage of source water has to be >= 0 and <= 100.");
    }
    else if(areainf < 0 || areainf > 100)
    {
        document.getElementById('admintblCommunityareaInf').style.backgroundColor="red";
        alert("Percentage of infrastructure has to be >= 0 and <= 100.");
    }
    else if(agc < 0 || agc > 100)
    {
        document.getElementById('admintblCommunityareaAgC').style.backgroundColor="red";
        alert("Percentage of commercial agriculture has to be >= 0 and <= 100.");
    }
    else if (agh < 0 || agh > 100) {
        document.getElementById('admintblCommunityareaAgH').style.backgroundColor = "red";
        alert("Percentage of hobby/subsistence agriculture has to be >= 0 and <= 100.");
    }
    else if (paved < 0 || paved > 100) {
        document.getElementById('admintblCommunityareaPaved').style.backgroundColor = "red";
        alert("Percentage of paved cover has to be >= 0 and <= 100.");
    }
    else if(forest < 0 || forest > 100)
    {
        document.getElementById('admintblCommunityareaForest').style.backgroundColor="red";
        alert("Percentage of forest has to be >= 0 and <= 100.");
    }
    else if(bestm < 0 || bestm > 100)
    {
        document.getElementById('admintblCommunityareaBM').style.backgroundColor="red";
        alert("Percentage of under best management practices has to be >= 0 and <= 100.");
    }
    else
    {
        if((forest + ag + areainf + sw + wet) > 100)
        {
            document.getElementById('admintblCommunityareaForest').style.backgroundColor="red";
            document.getElementById('admintblCommunityareaAgC').style.backgroundColor = "red";
            document.getElementById('admintblCommunityareaAgH').style.backgroundColor = "red";
            document.getElementById('admintblCommunityareaPaved').style.backgroundColor = "red";
            document.getElementById('admintblCommunityareaInf').style.backgroundColor="red";
            document.getElementById('admintblCommunityareaSW').style.backgroundColor="red";
            document.getElementById('admintblCommunityareaWet').style.backgroundColor="red";
            alert("Sum of percentages in Forest, Commercial Agriculture, Hobby/Subsistence Agriculture, Paved Cover, Infrastructure, Source Water and Wetlands cannot be over 100.");
        }
        else
        {
            document.getElementById('admintblCommunityareaForest').style.backgroundColor="white";
            document.getElementById('admintblCommunityareaAgC').style.backgroundColor = "white";
            document.getElementById('admintblCommunityareaAgH').style.backgroundColor = "white";
            document.getElementById('admintblCommunityareaPaved').style.backgroundColor = "white";
            document.getElementById('admintblCommunityareaInf').style.backgroundColor="white";
            document.getElementById('admintblCommunityareaSW').style.backgroundColor="white";
            document.getElementById('admintblCommunityareaWet').style.backgroundColor="white";
            document.getElementById('admintblCommunityareaNat').style.backgroundColor="white";
            document.getElementById('admintblCommunityareaProt').style.backgroundColor="white";
            document.getElementById('admintblCommunityareaBM').style.backgroundColor="white";
        }
    }
	
}