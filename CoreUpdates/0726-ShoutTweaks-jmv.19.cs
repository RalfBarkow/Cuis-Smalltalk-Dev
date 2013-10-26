'From Cuis 2.9 of 5 November 2010 [latest update: #634] on 22 December 2010 at 3:52:19 pm'!!classDefinition: #SHTextStyler category: #'Shout-Styling'!Object subclass: #SHTextStyler	instanceVariableNames: 'sem backgroundProcess text monitor view stylingEnabled textModel formattedText '	classVariableNames: ''	poolDictionaries: ''	category: 'Shout-Styling'!!classDefinition: #SHTextStylerST80 category: #'Shout-Styling'!SHTextStyler subclass: #SHTextStylerST80	instanceVariableNames: 'classOrMetaClass workspace parser formatAssignments sourceMap processedSourceMap disableFormatAndConvert '	classVariableNames: ''	poolDictionaries: ''	category: 'Shout-Styling'!!Behavior methodsFor: 'accessing' stamp: 'jmv 12/22/2010 13:44'!sourceCodeTemplate	"Answer an expression to be edited and evaluated in order to define 	methods in this class.	Modified to be parseable for Shour"	^'messageSelectorAndArgumentNames	"comment stating purpose of message"	| temporary variable names |	statements'! !!Debugger methodsFor: '*Shout-Styling' stamp: 'jmv 12/22/2010 15:13'!shoutAboutToStyle: aSHTextStyler	"This is a notification that aSHTextStyler is about to re-style its text.	Set the classOrMetaClass in aSHTextStyler, so that identifiers	will be resolved correctly.	Answer true to allow styling to proceed, or false to veto the styling"		self isModeStyleable ifFalse: [^false].	aSHTextStyler 		classOrMetaClass: self selectedClassOrMetaClass;		disableFormatAndConvert.	^true! !!MethodNode methodsFor: 'converting' stamp: 'jmv 12/22/2010 15:03'!asColorizedSmalltalk80Text	"Answer a colorized Smalltalk-80-syntax string description of the parse tree whose root is the receiver."	| printText |	printText _ self printString asText.	^(Smalltalk at: #SHTextStylerST80 ifAbsent: nil)		ifNotNil: [ :stylerClass |			stylerClass new				textModel: (TextModel new actualContents: printText);				formattedAndStyledText ]		ifNil: [ printText ]! !!SHTextStyler methodsFor: 'styling' stamp: 'jmv 12/22/2010 15:52'!formatAndStyle	"Do the styling on a copy of the model text.	After finishing, send it to the model, via #stylerStyled:checkForChanges:	The model should grab the TextAttributes we added to the copy, as appropriate."	self terminateBackgroundStylingProcess.	formattedText _ textModel actualContents.	self privateFormatAndConvert.	textModel actualContents: formattedText.	formattedText size < 4096		ifTrue: [			formattedText _ textModel actualContents copy.			textModel privateStyleWith: self.			textModel stylerStyled: formattedText checkForChanges: false ]		ifFalse: [ self styleInBackgroundProcess ].! !!SHTextStyler methodsFor: 'styling' stamp: 'jmv 12/22/2010 15:51'!formattedAndStyledText	"Answer a copy of the actualContents in the model that is both formatted and styled"	formattedText _ textModel actualContents.	self privateFormatAndConvert.	textModel privateStyleWith: self.	^formattedText! !!SHTextStyler methodsFor: 'styling' stamp: 'jmv 12/22/2010 15:51'!styleInBackgroundProcess	"Do the styling on a copy of the provided text (and in a separate process).	After finishing, send it to the model, via #stylerStyled:checkForChanges:	The the model should grab the TextAttributes we added to the copy, as appropriate."	self terminateBackgroundStylingProcess.	formattedText _ textModel actualContents copy.	self monitor critical: [		sem _ Semaphore new. 		[			sem ifNotNil: [				sem wait.				textModel stylerStyled: formattedText checkForChanges: true ]		] forkAt: Processor activePriority.		backgroundProcess _  [			textModel privateStyleWith: self.			sem signal] newProcess.		backgroundProcess priority: Processor userBackgroundPriority.		backgroundProcess resume	]! !!SHTextStyler methodsFor: 'private' stamp: 'jmv 12/22/2010 14:33'!privateFormatAndConvert	self shouldBeImplemented! !!SHTextStyler methodsFor: 'private' stamp: 'jmv 12/22/2010 14:23'!privateStyle	self shouldBeImplemented! !!SHTextStyler methodsFor: 'accessing' stamp: 'jmv 12/22/2010 14:07'!textModel: aTextModel	textModel _ aTextModel! !!SHTextStylerST80 methodsFor: 'private' stamp: 'jmv 12/22/2010 15:21'!convertAssignmentsToAnsi	"If the Preference is to show ansiAssignments then answer a copy of  <aText> where each  left arrow assignment is replaced with a ':=' ansi assignment. A parser is used so that each left arrow is only replaced if it occurs within an assigment statement"	formattedText _ self replaceStringForRangesWithType: #assignment with: ':=' in: formattedText! !!SHTextStylerST80 methodsFor: 'private' stamp: 'jmv 12/22/2010 15:21'!convertAssignmentsToLeftArrow	"If the Preference is to show leftArrowAssignments then answer a copy of  <aText> where each ansi assignment (:=) is replaced with a left arrow. A parser is used so that each ':=' is only replaced if it actually occurs within an assigment statement"	formattedText _ self replaceStringForRangesWithType: #ansiAssignment with: '_' in: formattedText! !!SHTextStylerST80 methodsFor: 'private' stamp: 'jmv 12/22/2010 15:24'!privateFormatAndConvert	"Perform any formatting of formattedText necessary and store or a formatted copy in formattedText"	Preferences syntaxHighlightingAsYouTypeAnsiAssignment ifTrue: [		self convertAssignmentsToAnsi ].	Preferences syntaxHighlightingAsYouTypeLeftArrowAssignment ifTrue: [		self convertAssignmentsToLeftArrow ]! !!SHTextStylerST80 methodsFor: 'private' stamp: 'jmv 12/22/2010 14:50'!privateStyle	| ranges |	ranges_ self rangesIn: formattedText setWorkspace: true.	ranges ifNotNil: [ self setAttributesIn: formattedText fromRanges: ranges ]! !!SHTextStylerST80 methodsFor: 'private' stamp: 'jmv 12/22/2010 15:16'!replaceStringForRangesWithType: aSymbol with: aString in: aText 	"Answer aText if no replacements, or a copy of aText with 	each range with a type of aSymbol replaced by aString"	| answer toReplace increaseInLength |	"We don't handle format and conversion for debuggers"	disableFormatAndConvert ifTrue: [ ^aText ].		toReplace := (self rangesIn: aText setWorkspace: false) 		select: [:each | each type = aSymbol].	toReplace isEmpty ifTrue: [^aText].	answer := aText copy.	increaseInLength := 0.	(toReplace asArray sort: [:a :b | a start <= b start]) 		do: [:each | | end start thisIncrease | 			start := each start + increaseInLength.			end := each end + increaseInLength.			answer 	replaceFrom: start to: end with: aString.			thisIncrease := aString size - each length.			increaseInLength := increaseInLength + thisIncrease ].	^answer! !!SHTextStylerST80 methodsFor: 'private' stamp: 'jmv 12/22/2010 10:39'!setAttributesIn: aText fromRanges: ranges	"modified by jmv to keep existing attributes if they answer true to #isParagraphAttribute"	| attributes defaultAttributes paragraphAttributes attr newRuns newValues lastAttr lastCount | 			defaultAttributes := self attributesFor: #default.	paragraphAttributes := Array new: aText size.	1 to: paragraphAttributes size do: [ :i |		paragraphAttributes at: i put:			((aText attributesAt: i) select: [ :each | each isParagraphAttribute ])].	attributes := Array new: aText size.	1 to: attributes size do: [ :i | attributes at: i put: (paragraphAttributes at: i), defaultAttributes].	ranges do: [ :range |		(attr := self attributesFor: range type)			ifNotNil: [ range start to: range end do: [:i |				attributes at: i put: (paragraphAttributes at: i), attr]]].	newRuns := OrderedCollection new: attributes size // 10.	newValues := OrderedCollection new: attributes size // 10.	1 to: attributes size do: [:i |		attr := attributes at: i.		i = 1 			ifTrue: [				newRuns add: 1.				lastCount := 1.				lastAttr := newValues add: attr]			ifFalse:[				attr == lastAttr					ifTrue: [						lastCount := lastCount + 1.						newRuns at: newRuns size put: lastCount]					ifFalse: [						newRuns add: 1.						lastCount := 1.						lastAttr := newValues add: attr]]].	aText privateSetRuns: (RunArray runs: newRuns values: newValues)	! !!SHTextStylerST80 methodsFor: 'accessing' stamp: 'jmv 12/22/2010 15:13'!disableFormatAndConvert	disableFormatAndConvert _ true! !!SHTextStylerST80 methodsFor: 'initialize-release' stamp: 'jmv 12/22/2010 15:13'!initialize	super initialize.	disableFormatAndConvert _ false! !!Text methodsFor: 'as yet unclassified' stamp: 'jmv 12/22/2010 10:45'!textStyleChunksDo: aBlock	"Evaluate aBlock over each chunk (sequence of paragraphs) that have the same textStyle"	| start nextStart style |	start _ 1.	nextStart _ 1.	[ start <= self size ] whileTrue: [		style _ self textStyleAt: start.		[ nextStart <= self size and: [ (self textStyleAt: nextStart) = style ]] whileTrue: [			nextStart _nextStart + 1 ].		aBlock value: (start to: nextStart-1) value: style.		start _ nextStart ]! !!TextModel methodsFor: 'shout support' stamp: 'jmv 12/22/2010 15:51'!privateStyleWith: aSHTextStyler	aSHTextStyler privateStyle! !!TextModel methodsFor: 'shout support' stamp: 'jmv 12/22/2010 15:34'!stylerStyled: styledCopyOfText checkForChanges: aBoolean	"If asked, check for changes. If the argument string is no longer valid (i.e., our contents have changed) then ignore the call."	aBoolean ifTrue: [		actualContents string = styledCopyOfText string			ifFalse: [ ^self ]].			actualContents privateSetRuns: styledCopyOfText runs.	self changed: #shoutStyle! !!TextModelMorph methodsFor: 'initialization' stamp: 'jmv 12/22/2010 14:08'!model: aTextModel	editorClass _ aTextModel editorClass.	super model: aTextModel.	textMorph		model: model wrappedTo: self viewableWidth.	model refetch.	styler ifNotNil: [ styler textModel: model ].	self formatAndStyleIfNeeded.		self setSelection: model getSelection! !!TextModelMorph methodsFor: 'menu commands' stamp: 'jmv 12/22/2010 13:48'!accept	"Inform the model of text to be accepted, and return true if OK."	"sps 8/13/2001 22:41: save selection and scroll info"	| ok saveSelection saveScrollerOffset |	saveSelection := self selectionInterval copy.	saveScrollerOffset := scroller offset copy.	(self canDiscardEdits and: [(self hasProperty: #alwaysAccept) not]) 		ifTrue: [^self flash].	self hasEditingConflicts 		ifTrue: [			(self 				confirm: 'Caution!! This method may have beenchanged elsewhere since you startedediting it here.  Accept anyway?' 						translated) 					ifFalse: [^self flash]].	ok := model acceptFrom: self.	ok == true 		ifTrue: [			model refetch.			self formatAndStyleIfNeeded.			self hasUnacceptedEdits: false ].	"sps 8/13/2001 22:41: restore selection and scroll info"		["During the step for the browser, updatePaneIfNeeded is called, and 		invariably resets the contents of the codeholding PluggableTextMorph		at that time, resetting the cursor position and scroller in the process.		The following line forces that update without waiting for the step, 		then restores the cursor and scrollbar"	ok 		ifTrue: [			"(don't bother if there was an error during compile)"			model updatePaneIfNeeded.			"jmv - moved this outside the deferred message.			See 'Re: [squeak-dev] scrambled input fields'			from Gary Chambers on Nov 14, 2008."			self selectFrom: saveSelection first to: saveSelection last.			WorldState addDeferredUIMessage: [					self currentHand newKeyboardFocus: textMorph.					scroller offset: saveScrollerOffset.					self setScrollDeltas.					"self selectFrom: saveSelection first to: saveSelection last"]]] 			on: Error			do: nil! !!TextModelMorph methodsFor: 'menu commands' stamp: 'jmv 12/22/2010 13:48'!cancel	model refetch.	self formatAndStyleIfNeeded.	self setSelection: model getSelection.	self hasUnacceptedEdits: false ! !!TextModelMorph methodsFor: 'unaccepted edits' stamp: 'jmv 12/22/2010 14:49'!hasUnacceptedEdits: aBoolean	"Set the hasUnacceptedEdits flag to the given value. "	aBoolean == hasUnacceptedEdits ifFalse: [		hasUnacceptedEdits _ aBoolean.		self changed].	aBoolean ifFalse: [hasEditingConflicts _ false].	"shout:  re-style the text iff aBoolean is true	Do not apply any formatting (i.e. changes to the characters in the text),	just styling (i.e. TextAttributes)"	(aBoolean and: [self okToStyle])		ifTrue: [ styler styleInBackgroundProcess ]! !!TextModelMorph methodsFor: 'updating' stamp: 'jmv 12/22/2010 15:35'!update: aSymbol 	aSymbol ifNil: [^self].	aSymbol == #flash ifTrue: [^self flash].	aSymbol == #acceptedContents 		ifTrue: [			model refetch.			self formatAndStyleIfNeeded.			"Some day, it would be nice to keep objects and update them			instead of throwing them away all the time for no good reason..."			textMorph releaseParagraph.			^self setSelection: model getSelection].	aSymbol == #initialSelection 		ifTrue: [^self setSelection: model getSelection].	aSymbol == #autoSelect 		ifTrue: [			self handleEdit: [					TextEditor abandonChangeText.	"no replacement!!"					self editor						setSearch: model autoSelectString;						againOrSame: true ]].	aSymbol == #clearUserEdits ifTrue: [^self hasUnacceptedEdits: false].	aSymbol == #wantToChange 		ifTrue: [			self canDiscardEdits ifFalse: [^self promptForCancel].			^self].	aSymbol == #appendEntry 		ifTrue: [			self handleEdit: [self appendEntry].			^self refreshWorld ].	aSymbol == #clearText 		ifTrue: [			self handleEdit: [self changeText: Text new].			textMorph releaseParagraph.			^self refreshWorld ].	aSymbol == #codeChangedElsewhere 		ifTrue: [			self hasEditingConflicts: true.			^self changed ].	aSymbol == #shoutStyle		ifTrue: [			self stylerStyled.			^self changed ].! !!TextModelMorph methodsFor: 'shout' stamp: 'jmv 12/22/2010 15:04'!formatAndStyleIfNeeded	"Apply both formatting (changes to the characters in the text, such as	preferred assignment operators), and styling (TextAttributes to make	Smalltalk code easier to understand)"	self okToStyle ifTrue: [		styler formatAndStyle ]! !!TextModelMorph methodsFor: 'shout' stamp: 'jmv 12/22/2010 14:14'!styler: anObject		styler := anObject.	styler ifNotNil: [ model ifNotNil: [ styler textModel: model ]]! !!TextModelMorph methodsFor: 'shout' stamp: 'jmv 12/22/2010 15:37'!stylerStyled	"jmv - 11/2010 The next line used to be commented out but it is needed to fix lines whose	length might change due to font change (i.e., styled comments are shorter than unstyled)"	textMorph paragraph recomposeFrom: 1 to:  model actualContents size delta: 0.	"older comment: caused chars to appear in wrong order esp. in demo mode. remove this line when sure it is fixed"		textMorph updateFromParagraph.	selectionInterval 		ifNotNil: [			self editor				selectInvisiblyFrom: selectionInterval first to: selectionInterval last;				storeSelectionInParagraph;				setEmphasisHereFromText].	self editor blinkParen.	self scrollSelectionIntoView! !!TextModelMorph class methodsFor: 'instance creation' stamp: 'jmv 12/22/2010 15:45'!textProvider: aTextProvider textGetter: textGetter textSetter: textSetter selectionGetter: selectionGetter	| styler newModel answer |	answer _ self new.	(Preferences syntaxHighlightingAsYouType 			and: [  aTextProvider is: #ShoutEnabled ]) ifTrue: [		styler _ SHTextStylerST80 new.		answer styler: styler ].	newModel _ PluggableTextModel on: aTextProvider.	newModel textGetter: textGetter textSetter: textSetter selectionGetter: selectionGetter.	aTextProvider addDependent: newModel.	answer model: newModel.	^answer! !!TextModelMorph class methodsFor: 'instance creation' stamp: 'jmv 12/22/2010 15:45'!withModel: aTextModel	|  answer styler |	answer _ self new.	(Preferences syntaxHighlightingAsYouType 			and: [ aTextModel is: #ShoutEnabled ]) ifTrue: [		styler _ SHTextStylerST80 new.		answer styler: styler ].	answer model: aTextModel.	^answer! !!Workspace methodsFor: 'testing' stamp: 'jmv 12/21/2010 23:20'!is: aSymbol	^ aSymbol == #ShoutEnabled or: [ super is: aSymbol ]! !TextModelMorph removeSelector: #maybeStyle!TextModelMorph removeSelector: #stylerStyled:!TextModelMorph removeSelector: #stylerStyledInBackground:!!TextModel reorganize!('initialize-release' initialize openAsMorphLabel: openInMorphicWindowLabeled:wrap: openLabel: openLabel:wrap:)('accessing' actualContents actualContents: contents:)('pane menu' editorClass perform:orSendTo:)('user edits' clearUserEditFlag)('services')('self-updating' updatePaneIfNeeded)('misc' getSelection refetch)('testing' wantsFrameAdornments)('commands' acceptFrom:)('shout support' privateStyleWith: stylerStyled:checkForChanges:)!SHTextStylerST80 removeSelector: #convertAssignmentsToAns!SHTextStylerST80 removeSelector: #convertAssignmentsToAnsi:!SHTextStylerST80 removeSelector: #convertAssignmentsToLeftArrow:!SHTextStylerST80 removeSelector: #parseableSourceCodeTemplate!SHTextStylerST80 removeSelector: #privateFormat:!SHTextStylerST80 removeSelector: #privateFormatAndConvert:!SHTextStylerST80 removeSelector: #privateStyle:!SHTextStylerST80 removeSelector: #sourceMap:!!classDefinition: #SHTextStylerST80 category: #'Shout-Styling'!SHTextStyler subclass: #SHTextStylerST80	instanceVariableNames: 'classOrMetaClass workspace parser disableFormatAndConvert'	classVariableNames: ''	poolDictionaries: ''	category: 'Shout-Styling'!SHTextStyler removeSelector: #format:!SHTextStyler removeSelector: #formatAndConvert!SHTextStyler removeSelector: #formatAndConvert:!SHTextStyler removeSelector: #initialize!SHTextStyler removeSelector: #privateFormat:!SHTextStyler removeSelector: #privateFormatAndConvert:!SHTextStyler removeSelector: #privateStyle:!SHTextStyler removeSelector: #style!SHTextStyler removeSelector: #style:!SHTextStyler removeSelector: #styleInBackgroundProcess:!SHTextStyler removeSelector: #styledText!SHTextStyler removeSelector: #styledTextFo!SHTextStyler removeSelector: #styledTextFor:!SHTextStyler removeSelector: #view:!!classDefinition: #SHTextStyler category: #'Shout-Styling'!Object subclass: #SHTextStyler	instanceVariableNames: 'sem backgroundProcess monitor formattedText textModel'	classVariableNames: ''	poolDictionaries: ''	category: 'Shout-Styling'!