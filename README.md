# ACSTagGen
Manager barcode generator for ACS-IR. Using barcodelib and pdfsharp, this program generates barcodes based on the user input. It then creats a pdf file that they can save or print. 

# How to use
* Operator number should be between 1 and 999.
* Password is a 2 digit passord. If inputting only 1 digit the program will pad it with a zero in the front. 
* Clicking generate will diplay a preview of the barcode and add it to the list of barcodes to save.
* Remove will remove the selcted barcode. 
* Save selected will save the selected barcode to a pdf file.
* Save all will save all the barcodes in the list to a pdf file. 
* Checking the Show Number on Barcode box will show the operator number on the barcode when the pdf is generated.
* Password format type
  * uuupp
