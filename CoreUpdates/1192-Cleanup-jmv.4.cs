'From Cuis 4.0 of 16 November 2011 [latest update: #1144] on 2 January 2012 at 4:00:16 pm'!!Inspector methodsFor: 'accessing' stamp: 'jmv 1/2/2012 15:00'!   update	"Reshow contents, assuming selected value may have changed."	selectionIndex = 0		ifFalse: [			self contentsIsString				ifTrue: [ acceptedContentsCache _ self selection]				ifFalse: [ acceptedContentsCache _ self selectionPrintString].			self acceptedContentsChanged.			self changed: #selectionIndex ]! !!Inspector methodsFor: 'selecting' stamp: 'jmv 1/2/2012 15:00'!                               toggleIndex: anInteger	"The receiver has a list of variables of its inspected object. One of these 	is selected. If anInteger is the index of this variable, then deselect it. 	Otherwise, make the variable whose index is anInteger be the selected 	item."	selectionIndex = anInteger		ifTrue: [			"same index, turn off selection"			selectionIndex _ 0.			acceptedContentsCache _ '']		ifFalse: [			"different index, new selection"			selectionIndex _ anInteger.			self contentsIsString				ifTrue: [ acceptedContentsCache _ self selection]				ifFalse: [ acceptedContentsCache _ self selectionPrintString]].	self acceptedContentsChanged.	self changed: #selectionIndex! !!DictionaryInspector methodsFor: 'menu' stamp: 'jmv 1/2/2012 15:00'!               removeSelection	selectionIndex = 0 ifTrue: [^ self changed: #flash].	object removeKey: (keyArray at: selectionIndex - self numberOfFixedFields).	selectionIndex := 0.	acceptedContentsCache _ ''.	self calculateKeyArray.	self changed: #inspectObject.	self changed: #selectionIndex.	self changed: #fieldList! !!SetInspector methodsFor: 'menu' stamp: 'jmv 1/2/2012 15:00'!     removeSelection	(selectionIndex <= object class instSize) ifTrue: [^ self changed: #flash].	object remove: self selection.	selectionIndex := 0.	acceptedContentsCache _ ''.	self changed: #inspectObject.	self changed: #fieldList.	self changed: #selectionIndex.! !!TextModelMorph methodsFor: 'updating' stamp: 'jmv 1/2/2012 15:19'!             update: aSymbol	super update: aSymbol.	aSymbol ifNil: [^self].	aSymbol == #flash ifTrue: [^self flash].	aSymbol == #actualContents ifTrue: [		"Some day, it would be nice to keep objects and update them		instead of throwing them away all the time for no good reason..."		self textMorph releaseEditorAndParagraph.		self textMorph formatAndStyleIfNeeded.		^self ].	aSymbol == #acceptedContents ifTrue: [		self textMorph hasUnacceptedEdits ifTrue: [			self textMorph hasEditingConflicts: true.			^self redrawNeeded ].		model refetch.		self setScrollDeltas.		^self redrawNeeded ].	aSymbol == #refetched ifTrue: [		self setSelection: model getSelection.		self hasUnacceptedEdits: false.		^self ].	aSymbol == #initialSelection ifTrue: [		^self setSelection: model getSelection; redrawNeeded ].	aSymbol == #autoSelect ifTrue: [		TextEditor abandonChangeText.	"no replacement!!"		self editor			setSearch: model autoSelectString;			findAndReplaceMany: true.		self textMorph updateFromParagraph.		^self scrollSelectionIntoView ].	"Quite ugly"	aSymbol == #clearUserEdits ifTrue: [		^self hasUnacceptedEdits: false].	aSymbol == #shoutStyle ifTrue: [		self textMorph stylerStyled.		^self redrawNeeded ]! !