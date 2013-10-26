'From Cuis 1.0 of 6 April 2009 [latest update: #169] on 8 April 2009 at 8:42:27 am'!!TextEditor methodsFor: 'menu messages' stamp: 'jmv 4/8/2009 08:41'!saveContentsInFile	"Save the receiver's contents string to a file, prompting the user for a file-name.  Suggest a reasonable file-name."	| fileName stringToSave parentWindow labelToUse suggestedName lastIndex |	stringToSave := paragraph text string.	stringToSave size = 0 ifTrue: [^self inform: 'nothing to save.'].	parentWindow := model dependents 				detect: [:dep | dep isKindOf: SystemWindow]				ifNone: [nil].	labelToUse := parentWindow ifNil: ['Untitled']				ifNotNil: [parentWindow label].	suggestedName := nil.	#(#('Decompressed contents of: ' '.gz')) do: 			[:leaderTrailer | 			"can add more here..."			(labelToUse beginsWith: leaderTrailer first) 				ifTrue: 					[suggestedName := labelToUse copyFrom: leaderTrailer first size + 1								to: labelToUse size.					(labelToUse endsWith: leaderTrailer last) 						ifTrue: 							[suggestedName := suggestedName copyFrom: 1										to: suggestedName size - leaderTrailer last size]						ifFalse: 							[lastIndex := suggestedName lastIndexOf: $. ifAbsent: [0].							(lastIndex = 0 or: [lastIndex = 1]) 								ifFalse: [suggestedName := suggestedName copyFrom: 1 to: lastIndex - 1]]]].	suggestedName ifNil: [suggestedName := labelToUse , '.text'].	fileName := FillInTheBlank request: 'File name?'				initialAnswer: suggestedName.	fileName isEmptyOrNil 		ifFalse: 			[(FileStream newFileNamed: fileName)				nextPutAll: stringToSave;				close]! !!Workspace methodsFor: 'as yet unclassified' stamp: 'jmv 4/8/2009 08:41'!saveContentsInFile	"A bit of a hack to pass along this message to the controller or morph.  (Possibly this Workspace menu item could be deleted, since it's now in the text menu.)"	| textMorph |	textMorph := self dependents 				detect: [:dep | dep isKindOf: PluggableTextMorph]				ifNone: [nil].	textMorph notNil ifTrue: [^textMorph saveContentsInFile]! !