'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 18 May 2011 at 5:27:19 am'!"Change Set:		0X+3-IconPack-unload-cbrDate:			18 May 2011Author:			Casey RansbergerIconPack is just an artifact to be recorded in the update stream. It is not required in the image after loading the icons from it. This change set unloads it."!Smalltalk removeClassNamed: #IconPack!