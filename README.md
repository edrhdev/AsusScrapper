# AsusScrapper

This idea was proposed by [dragonn_7642](https://discord.com/channels/725125934759411753/768083676952657990/1129772761343078481) in the [ASUS-Linux Discord](https://discord.gg/BY5ucafD)

It was on python but I feel more comfortable using C#.

The main idea is to scan batches of generated urls of official BIOS update versions, to get all the unlisted (hidden) ones and try to downgrade with an older version to fix problems caused by upgrading the BIOS of several ASUS laptops.

In my case this code was generating batches of the following URL:

https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/111812/G713QRAS331.zip

Which is the latest bios update version of my ASUS Rog Strix G17 (G713QR).

Based on that link, we can generate the older batches and then scan all of them to get the ones that are still stored in ASUS servers.

https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/{randomNumber}/G713QRAS{version}.zip

The program asks for first and last BIOS versions you want to find.

Then it asks for a begin and end identifier, this is a number we think is a row of an SQL column where the URLs are being stored in the ASUS servers, so this number is generated in a range and test all of them for each version we want to find.

I ran the program with the following params:

1. First version: 290
2. Last version: 331 (the one I know for sure that exists because it's the latest available on the asus drivers website)
3. Start id: 90000 (the initial random number we think it's a column id on a database)
4. End id: 112000 (higher than the last number of the last known existing version, just to be sure)
5. Batches: 1000 (amount of tasks to create with the head requests, I tried with 5k and it runs even faster but takes more ram).

You will need to customize the url to match with your URL format of the BIOS version you can get in the asus drivers website (your specific model, last version and the minimum you want to try).

I got the following URLs:

https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/111812/G713QRAS331.zip
https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/108224/G713QRAS330.zip
https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/107412/G713QRAS329.zip
https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/106276/G713QRAS327.zip
https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/103784/G713QRAS325.zip
https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/102484/G713QRAS323.zip
https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/102085/G713QRAS321.zip
https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/101694/G713QRAS320.zip

This won't assure your bios/hardware problems will get fixed, this just let's you get the older official hidden BIOS update versions in case you need them.

Feel free to fork this and share with whoever may need it.
