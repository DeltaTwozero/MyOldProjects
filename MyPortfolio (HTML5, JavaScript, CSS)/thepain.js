var image = document.getElementById('gal');
var index = 0;

function gallery()
{
	//console.log("test");
	if(index == 0)
	{
        image.src ="hl/hl2.jpg";
		index++;
	}
	else if (index == 1)
	{
        image.src ="hl/hl3.png";
		index++;
    }
    else if (index == 2)
    {
        image.src = "sc/sc1.jpg";
        index++;
    }
    else if (index == 3)
    {
        image.src = "sc/sc2.jpg";
        index++;
    }
    else if (index == 4)
    {
        image.src = "sc/sc3.jpg";
        index++;
    }
    else if (index == 5)
    {
        image.src = "cards/card1.jpg";
        index++;
    }
    else if (index == 6)
    {
        image.src = "cards/card2.jpg";
        index++;
    }
    else if (index == 7)
    {
        image.src = "cards/card3.jpg";
        index++;
    }
	else
	{
		image.src="hl/hl1.jpg";
		index = 0;
	}
	setTimeout(gallery, 3000);
}
setTimeout(gallery, 3000);