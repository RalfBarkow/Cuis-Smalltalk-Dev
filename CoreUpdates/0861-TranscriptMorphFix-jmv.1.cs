'From Cuis 3.1 of 4 March 2011 [latest update: #850] on 14 March 2011 at 10:20:44 am'!!Morph methodsFor: 'updating' stamp: 'jmv 3/14/2011 10:19'!update: aSymbol	"Please do call super in subclasses (unless you do have a reason not to)"	aSymbol == #redraw ifTrue: [		self redrawNeeded ]! !!PluggableListMorph methodsFor: 'updating' stamp: 'jmv 3/14/2011 10:17'!update: aSymbol 	"Refer to the comment in View|update:."	super update: aSymbol.	aSymbol == getListSelector ifTrue: [		self updateList.		^ self].	aSymbol == getIndexSelector ifTrue: [		self selectionIndex: self getCurrentSelectionIndex ]! !!PluggableListMorphOfMany methodsFor: 'updating' stamp: 'jmv 3/14/2011 10:17'!update: aSymbol 	super update: aSymbol.	aSymbol == #allSelections ifTrue: [		self selectionIndex: self getCurrentSelectionIndex.		self redrawNeeded]! !!SimpleHierarchicalListMorph methodsFor: 'updating' stamp: 'jmv 3/14/2011 10:16'!update: aSymbol	super update: aSymbol.	aSymbol == getSelectionSelector 		ifTrue: [			self selection: self getCurrentSelectionItem.			^self].	aSymbol == getListSelector 		ifTrue: [			self list: self getList.			^self].	((aSymbol isKindOf: Array) 		and: [ aSymbol notEmpty and: [aSymbol first == #openPath]]) 			ifTrue: [				^(scroller submorphs at: 1 ifAbsent: [^self]) 					openPath: aSymbol allButFirst]! !!SystemWindow methodsFor: 'label' stamp: 'jmv 3/14/2011 10:17'!update: aSymbol	super update: aSymbol.	aSymbol = #relabel		ifTrue: [ model ifNotNil: [ self setLabel: model labelString ]]! !!TextModelMorph methodsFor: 'updating' stamp: 'jmv 3/14/2011 10:16'!update: aSymbol	super update: aSymbol.	aSymbol ifNil: [^self].	aSymbol == #flash ifTrue: [^self flash].	aSymbol == #actualContents 		ifTrue: [			"Some day, it would be nice to keep objects and update them			instead of throwing them away all the time for no good reason..."			textMorph releaseParagraph.			self formatAndStyleIfNeeded.			^self].	aSymbol == #acceptedContents ifTrue: [		model refetch.		^self].	aSymbol == #refetched ifTrue: [		self setSelection: model getSelection.		self hasUnacceptedEdits: false.		^self].	aSymbol == #initialSelection 		ifTrue: [^self setSelection: model getSelection].	aSymbol == #autoSelect 		ifTrue: [			self handleEdit: [					TextEditor abandonChangeText.	"no replacement!!"					self editor						setSearch: model autoSelectString;						againOrSame: true ]].	aSymbol == #clearUserEdits ifTrue: [^self hasUnacceptedEdits: false].	aSymbol == #wantToChange 		ifTrue: [			self canDiscardEdits ifFalse: [^self promptForCancel].			^self].	aSymbol == #codeChangedElsewhere 		ifTrue: [			self hasEditingConflicts: true.			^self redrawNeeded ].	aSymbol == #shoutStyle		ifTrue: [			self stylerStyled.			^self redrawNeeded ].! !!Transcript class methodsFor: 'displaying' stamp: 'jmv 3/14/2011 10:19'!display	showOnDisplay ifTrue: [		self displayOn: Display.		lastDisplayTime _ DateAndTime now ].	self changed: #redraw	"So any morph in front of us is repaired when Morphic cycles"! !