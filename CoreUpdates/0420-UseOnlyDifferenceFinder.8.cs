'From Cuis 2.0 of 12 February 2010 [latest update: #418] on 12 February 2010 at 4:45:59 pm'!!CodeHolder methodsFor: 'controls' stamp: 'jmv 2/12/2010 16:19'!contentsSymbolQuints	"Answer a list of quintuplets representing information on the alternative views available in the code pane		first element:	the contentsSymbol used		second element:	the selector to call when this item is chosen.		third element:	the selector to call to obtain the wording of the menu item.		fourth element:	the wording to represent this view		fifth element:	balloon help	A hypen indicates a need for a seperator line in a menu of such choices"	^ #((source				togglePlainSource 			showingPlainSourceString														'source'			'the textual source code as writen')(documentation		toggleShowDocumentation showingDocumentationString														'documentation'	'the first comment in the method')-(prettyPrint			togglePrettyPrint 			prettyPrintString														'prettyPrint'			'the method source presented in a standard text format')-(lineDiffs				toggleLineDiffing			showingLineDiffsString														'lineDiffs'			'the textual source lines diffed from its prior version')(wordDiffs			toggleWordDiffing			showingWordDiffsString														'wordDiffs'			'the textual source words diffed from its prior version')(prettyLineDiffs		togglePrettyLineDiffing	showingPrettyLineDiffsString														'prettyLineDiffs'		'formatted source lines diffed from formatted prior version')(prettyWordDiffs	togglePrettyWordDiffing	showingPrettyWordDiffsString														'prettyWordDiffs'	'formatted source words diffed from prior version')-(decompile			toggleDecompile			showingDecompileString														'decompile'			'source code decompiled from byteCodes')(byteCodes			toggleShowingByteCodes	showingByteCodesString														'byteCodes'		'the bytecodes that comprise the compiled method')	)! !!CodeHolder methodsFor: 'controls' stamp: 'jmv 2/12/2010 16:19'!sourceAndDiffsQuintsOnly	"Answer a list of quintuplets representing information on the alternative views available in the code pane for the case where the only plausible choices are showing source or either of the two kinds of diffs"	^ #((source				togglePlainSource 			showingPlainSourceString														'source'			'the textual source code as writen')(lineDiffs				toggleLineDiffing			showingLineDiffsString														'lineDiffs'			'the textual source diffed from its prior version')(wordDiffs			toggleWordDiffing			showingWordDiffsString														'wordDiffs'			'the textual source words diffed from its prior version')(prettyLineDiffs		togglePrettyLineDiffing	showingPrettyLineDiffsString														'linePrettyDiffs'		'formatted source diffed from formatted prior version')(prettyWordDiffs	togglePrettyWordDiffing	showingPrettyWordDiffsString														'linePrettyDiffs'		'formatted source words diffed from prior version')	)! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:01'!defaultDiffsSymbol	"Answer the code symbol to use when generically switching to diffing"	^ Preferences diffsWithPrettyPrint 		ifTrue: [			#prettyLineDiffs]		ifFalse: [			#lineDiffs]! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:05'!diffFromPriorSourceFor: sourceCode 	"If there is a prior version of source for the selected method, return a diff, else just return the source code"	^ self priorSourceOrNil		ifNil: [ sourceCode ]		ifNotNil: [ :prior |			DifferenceFinder				displayPatchFrom: prior to: sourceCode				tryWords: self shouldDiffWords				prettyPrintedIn: (self showingAnyKindOfPrettyDiffs ifTrue: [self selectedClass])]! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:24'!lineDiffButton	"Return a checkbox that lets the user decide whether regular diffs should be shown or not"	|  outerButton button |	outerButton _ AlignmentMorph proportional.	outerButton borderWidth: 2; borderColor: #raised.	outerButton color:  Color transparent.	button _ UpdatingThreePhaseButtonMorph checkBox.	button		target: self;		actionSelector: #toggleLineDiffing;		getSelector: #showingLineDiffs.	outerButton 		addMorph: button			fullFrame: (LayoutFrame fractions: (0@0 corner: 0@1) offsets: (2@3 corner: 18@0));		addMorph: (StringMorph contents: 'lineDiffs') lock			fullFrame: (LayoutFrame fractions: (0@0 corner: 1@1) offsets: (18@2 corner: 0@0)).	outerButton setBalloonText: 'If checked, then code differences from the previous version, if any, will be shown.'.	^ outerButton! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:24'!prettyLineDiffButton	"Return a checkbox that lets the user decide whether prettyDiffs should be shown or not"	|  outerButton button |	outerButton _ AlignmentMorph proportional.	outerButton borderWidth: 2; borderColor: #raised.	outerButton color:  Color transparent.	button _ UpdatingThreePhaseButtonMorph checkBox.	button		target: self;		actionSelector: #togglePrettyLineDiffing;		getSelector: #showingPrettyLineDiffs.	outerButton 		addMorph: button			fullFrame: (LayoutFrame fractions: (0@0 corner: 0@1) offsets: (2@3 corner: 18@0));		addMorph: (StringMorph contents: 'linePrettyDiffs') lock			fullFrame: (LayoutFrame fractions: (0@0 corner: 1@1) offsets: (18@2 corner: 0@0)).	(self isKindOf: VersionsBrowser)		ifTrue:			[outerButton setBalloonText: 'If checked, then pretty-printed code differences from the previous version, if any, will be shown.']		ifFalse:			[outerButton setBalloonText: 'If checked, then pretty-printed code differences between the file-based method and the in-memory version, if any, will be shown.'].	^ outerButton! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:23'!prettyWordDiffButton	"Return a checkbox that lets the user decide whether prettyDiffs should be shown or not"	|  outerButton button |	outerButton _ AlignmentMorph proportional.	outerButton borderWidth: 2; borderColor: #raised.	outerButton color:  Color transparent.	button _ UpdatingThreePhaseButtonMorph checkBox.	button		target: self;		actionSelector: #togglePrettyWordDiffing;		getSelector: #showingPrettyWordDiffs.	outerButton 		addMorph: button			fullFrame: (LayoutFrame fractions: (0@0 corner: 0@1) offsets: (2@3 corner: 18@0));		addMorph: (StringMorph contents: 'wordPrettyDiffs') lock			fullFrame: (LayoutFrame fractions: (0@0 corner: 1@1) offsets: (18@2 corner: 0@0)).	(self isKindOf: VersionsBrowser)		ifTrue:			[outerButton setBalloonText: 'If checked, then pretty-printed code differences (better algorithm) from the previous version, if any, will be shown.']		ifFalse:			[outerButton setBalloonText: 'If checked, then pretty-printed code differences (better algorithm) between the file-based method and the in-memory version, if any, will be shown.'].	^ outerButton! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:03'!shouldDiffWords	"Answer whether the receiver is currently set to use the alternative (slower but better) differ"	^ #(wordDiffs prettyWordDiffs) includes: contentsSymbol! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:22'!showLineDiffs: aBoolean	"Set whether I'm showing regular diffs as indicated"	self showingLineDiffs		ifFalse: [			aBoolean ifTrue:				[contentsSymbol _ #lineDiffs]]		ifTrue: [			aBoolean ifFalse:				[contentsSymbol _ #source]].	self setContentsToForceRefetch.	self contentsChanged! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:20'!showPrettyLineDiffs: aBoolean	"Set whether I'm showing pretty diffs as indicated"	self showingPrettyLineDiffs		ifFalse: [			aBoolean ifTrue: [				contentsSymbol _ #prettyLineDiffs]]		ifTrue: [			aBoolean ifFalse: [				contentsSymbol _ #source]].	self setContentsToForceRefetch.	self contentsChanged! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:20'!showPrettyWordDiffs: aBoolean	"Set whether I'm showing pretty diffs as indicated"	self showingPrettyWordDiffs		ifFalse: [			aBoolean ifTrue: [				contentsSymbol _ #prettyWordDiffs]]		ifTrue: [			aBoolean ifFalse: [				contentsSymbol _ #source]].	self setContentsToForceRefetch.	self contentsChanged! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:21'!showWordDiffs: aBoolean	"Set whether I'm showing regular diffs as indicated"	self showingWordDiffs		ifFalse: [			aBoolean ifTrue: [				contentsSymbol _ #wordDiffs]]		ifTrue: [			aBoolean ifFalse: [				contentsSymbol _ #source]].	self setContentsToForceRefetch.	self contentsChanged! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:03'!showingAnyKindOfDiffs	"Answer whether the receiver is currently set to show any kind of diffs"	^ #(lineDiffs prettyLineDiffs wordDiffs prettyWordDiffs) includes: contentsSymbol! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:03'!showingAnyKindOfPrettyDiffs	"Answer whether the receiver is currently set to show any kind of pretty diffs"	^ #(prettyLineDiffs prettyWordDiffs) includes: contentsSymbol! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:13'!showingLineDiffs	"Answer whether the receiver is showing regular diffs of source code"	^ contentsSymbol == #lineDiffs! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:13'!showingLineDiffsString	"Answer a string representing whether I'm showing regular diffs"	^ (self showingLineDiffs		ifTrue:			['<yes>']		ifFalse:			['<no>']), 'lineDiffs'! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:17'!showingPrettyLineDiffs	"Answer whether the receiver is showing pretty diffs of source code"	^ contentsSymbol == #prettyLineDiffs! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:17'!showingPrettyLineDiffsString	"Answer a string representing whether I'm showing pretty diffs"	^ (self showingPrettyLineDiffs		ifTrue:			['<yes>']		ifFalse:			['<no>']), 'linePrettyDiffs'! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:18'!showingPrettyWordDiffs	"Answer whether the receiver is showing pretty diffs of source code"	^ contentsSymbol == #prettyWordDiffs! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:19'!showingPrettyWordDiffsString	"Answer a string representing whether I'm showing pretty diffs"	^ (self showingPrettyWordDiffs		ifTrue:			['<yes>']		ifFalse:			['<no>']), 'wordPrettyDiffs'! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:15'!showingWordDiffs	"Answer whether the receiver is showing regular diffs (alternative algorithm) of source code"	^ contentsSymbol == #wordDiffs! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:15'!showingWordDiffsString	"Answer a string representing whether I'm showing regular diffs"	^ (self showingWordDiffs		ifTrue:			['<yes>']		ifFalse:			['<no>']), 'wordDiffs'! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:22'!toggleLineDiffing	"Toggle whether regular-diffing should be shown in the code pane"	| wasShowingDiffs |	self okToChange ifTrue: [		wasShowingDiffs _ self showingLineDiffs.		self showLineDiffs: wasShowingDiffs not.		self setContentsToForceRefetch.		self contentsChanged]! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:21'!togglePrettyLineDiffing	"Toggle whether pretty-diffing should be shown in the code pane"	| wasShowingDiffs |	self okToChange ifTrue: [		wasShowingDiffs _ self showingPrettyLineDiffs.		self showPrettyLineDiffs: wasShowingDiffs not.		self setContentsToForceRefetch.		self contentsChanged]! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:20'!togglePrettyWordDiffing	"Toggle whether pretty-diffing should be shown in the code pane"	| wasShowingDiffs |	self okToChange ifTrue: [		wasShowingDiffs _ self showingPrettyWordDiffs.		self showPrettyWordDiffs: wasShowingDiffs not.		self setContentsToForceRefetch.		self contentsChanged]! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:21'!toggleWordDiffing	"Toggle whether regular-diffing should be shown in the code pane"	| wasShowingDiffs |	self okToChange ifTrue: [		wasShowingDiffs _ self showingWordDiffs.		self showWordDiffs: wasShowingDiffs not.		self setContentsToForceRefetch.		self contentsChanged]! !!CodeHolder methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:34'!wordDiffButton	"Return a checkbox that lets the user decide whether regular diffs should be shown or not"	|  outerButton button |	outerButton _ AlignmentMorph proportional.	outerButton borderWidth: 2; borderColor: #raised.	outerButton color:  Color transparent.	button _ UpdatingThreePhaseButtonMorph checkBox.	button		target: self;		actionSelector: #toggleWordDiffing;		getSelector: #showingWordDiffs.	outerButton 		addMorph: button			fullFrame: (LayoutFrame fractions: (0@0 corner: 0@1) offsets: (2@3 corner: 18@0));		addMorph: (StringMorph contents: 'wordDiffs') lock			fullFrame: (LayoutFrame fractions: (0@0 corner: 1@1) offsets: (18@2 corner: 0@0)).	outerButton setBalloonText: 'If checked, then code differences (better algorithm) from the previous version, if any, will be shown.'.	^ outerButton! !!CodeHolder methodsFor: 'message list' stamp: 'jmv 2/12/2010 16:03'!sourceStringPrettifiedAndDiffed	"Answer a copy of the source code for the selected message, transformed by diffing and pretty-printing exigencies"	| class selector sourceString |	class _ self selectedClassOrMetaClass.	selector _ self selectedMessageName.	(class isNil or: [selector isNil]) ifTrue: [^ 'missing'].	sourceString _ class ultimateSourceCodeAt: selector ifAbsent: [^ 'error'].	self validateMessageSource: sourceString forSelector: selector.	(#(prettyPrint prettyLineDiffs prettyWordDiffs) includes: contentsSymbol) ifTrue: [		sourceString _ class compilerClass new						format: sourceString 						in: class 						notifying: nil].	self showingAnyKindOfDiffs ifTrue:		[sourceString _ self diffFromPriorSourceFor: sourceString].	^ sourceString! !!ChangeList methodsFor: 'menu actions' stamp: 'jmv 2/12/2010 16:04'!compareToCurrentVersion	"If the current selection corresponds to a method in the system, then spawn a window showing the diffs as text"	| change class s1 s2 differDesc diffWords |	listIndex = 0		ifTrue: [^ self].	change _ changeList at: listIndex.	((class _ change methodClass) notNil			and: [class includesSelector: change methodSelector])		ifTrue: [			s1 _ (class sourceCodeAt: change methodSelector) asString.			s2 _ change string.			s1 = s2				ifTrue: [^ self inform: 'Exact Match'].			diffWords _ self shouldDiffWords.			differDesc _ diffWords				ifTrue: [ 'Words']				ifFalse: [ 'Lines'].			(StringHolder new				textContents: (					(DifferenceFinder						displayPatchFrom: s1 to: s2						tryWords: diffWords						prettyPrintedIn: (self showingAnyKindOfPrettyDiffs ifTrue: [class]))							initialFont: Preferences standardCodeFont))				openLabel: 'Comparison to Current Version: ', differDesc, 					(self showingAnyKindOfPrettyDiffs ifTrue: [', using prettyPrint'] ifFalse: [''])]		ifFalse: [self flash]! !!ChangeList methodsFor: 'menu actions' stamp: 'jmv 2/12/2010 16:34'!optionalButtonRow	"Answer a row of buttons to occur in a tool pane"	| row buttons widths |	buttons _ OrderedCollection new.	widths _ OrderedCollection new.	self buttonSpecs do: [ :tuple | | button |		widths add: tuple first.		button _ PluggableButtonMorph 					model: self					stateGetter: nil					action: tuple third.		button			label: tuple second asString;			askBeforeChanging: true.		buttons add: button.		button setBalloonText: tuple fourth].	buttons add: self lineDiffButton.	widths add: 14.	buttons add: self wordDiffButton.	widths add: 16.	self wantsPrettyDiffOption ifTrue: [		buttons add:  self prettyLineDiffButton.		widths add: 21.		buttons add:  self prettyWordDiffButton.		widths add: 23 ].	row _ AlignmentMorph proportional.	row addInRow: buttons widthProportionalTo: widths.	^row! !!ChangeList methodsFor: 'viewing access' stamp: 'jmv 2/12/2010 16:04'!contentsDiffedFromCurrent	"Answer the contents diffed forward from current (in-memory) method version"	|  aChange aClass  name aSelector |	listIndex = 0		ifTrue: [^ ''].	aChange _ changeList at: listIndex.	((aChange type == #method 			and: [(aClass _ aChange methodClass) notNil]) 			and: [aClass includesSelector: aChange methodSelector]) ifTrue: [		aSelector _ aChange methodSelector.		(aClass notNil and: [aClass includesSelector: aSelector])			ifFalse: [ ^aChange text copy ].		^DifferenceFinder				displayPatchFrom: (aClass sourceCodeAt: aSelector)				to: aChange text				tryWords: self shouldDiffWords				prettyPrintedIn: (self showingAnyKindOfPrettyDiffs ifTrue: [aClass]) ].	(aChange type == #classDefinition and: [			name _ aChange methodClassName.			Smalltalk includesKey: name]) ifTrue: [		aClass _ Smalltalk at: name.		aChange isMetaClassChange ifTrue: [ aClass _ aClass class ].		^DifferenceFinder				displayPatchFrom: aClass definition to: aChange text tryWords: true].	^(changeList at: listIndex) text! !!ChangeList methodsFor: 'viewing access' stamp: 'jmv 2/12/2010 16:04'!diffedVersionContents	"Answer diffed version contents, maybe pretty maybe not"	| change class earlier later |	(listIndex = 0			or: [changeList size < listIndex])		ifTrue: [^ ''].	change _ changeList at: listIndex.	later _ change text.	class _ change methodClass.	(listIndex = changeList size or: [class == nil])		ifTrue: [^ later].	earlier _ (changeList at: listIndex + 1) text.	^DifferenceFinder		displayPatchFrom: earlier to: later		tryWords: self shouldDiffWords		prettyPrintedIn: (self showingAnyKindOfPrettyDiffs ifTrue: [class])! !!ChangeSorter methodsFor: 'code pane' stamp: 'jmv 2/12/2010 16:02'!setContents	"return the source code that shows in the bottom pane"	| sel class strm changeType |	self clearUserEditFlag.	currentClassName ifNil: [^ contents _ myChangeSet preambleString ifNil: ['']].	class _ self selectedClassOrMetaClass.	(sel _ currentSelector) == nil		ifFalse: [changeType _ (myChangeSet atSelector: (sel _ sel asSymbol) class: class).			changeType == #remove				ifTrue: [^ contents _ 'Method has been removed (see versions)'].			changeType == #addedThenRemoved				ifTrue: [^ contents _ 'Added then removed (see versions)'].			class ifNil: [^ contents _ 'Method was added, but cannot be found!!'].			(class includesSelector: sel)				ifFalse: [^ contents _ 'Method was added, but cannot be found!!'].			contents _ class sourceCodeAt: sel.			(#(prettyPrint prettyLineDiffs prettyWordDiffs) includes: contentsSymbol) ifTrue:				[contents _ class compilerClass new						format: contents 						in: class 						notifying: nil].			self showingAnyKindOfDiffs				ifTrue: [contents _ self diffFromPriorSourceFor: contents].			^ contents _ contents asText makeSelectorBoldIn: class]		ifTrue: [strm _ WriteStream on: (String new: 100).			(myChangeSet classChangeAt: currentClassName) do:				[:each |				each = #remove ifTrue: [strm nextPutAll: 'Entire class was removed.'; cr].				each = #addedThenRemoved ifTrue: [strm nextPutAll: 'Class was added then removed.'].				each = #rename ifTrue: [strm nextPutAll: 'Class name was changed.'; cr].				each = #add ifTrue: [strm nextPutAll: 'Class definition was added.'; cr].				each = #change ifTrue: [strm nextPutAll: 'Class definition was changed.'; cr].				each = #reorganize ifTrue: [strm nextPutAll: 'Class organization was changed.'; cr].				each = #comment ifTrue: [strm nextPutAll: 'New class comment.'; cr.				]].			^ contents _ strm contents].! !!DifferenceFinder methodsFor: 'private' stamp: 'jmv 2/12/2010 16:43'!linesAreSimilar	^self similitudeProportion > 0.3! !!DifferenceFinder class methodsFor: 'compatibility' stamp: 'jmv 2/12/2010 15:56'!displayPatchFrom: srcString to: dstString tryWords: aBoolean	| finder |	finder _ self base: srcString case: dstString.	finder compareLines; compute.	(aBoolean and: [ finder linesAreSimilar ])		ifTrue: [ finder recomputeWithWords ].	^finder differences anyOne asText! !!DifferenceFinder class methodsFor: 'compatibility' stamp: 'jmv 2/12/2010 15:53'!displayPatchFrom: srcString to: dstString tryWords: aBoolean prettyPrintedIn: aClass	| formattedSrcString formattedDstString |	formattedSrcString _ [		aClass compilerClass new			format: srcString			in: aClass			notifying: nil ] 				on: Error				do: [ :ex | srcString ].	formattedDstString _ [		aClass compilerClass new			format: dstString			in: aClass			notifying: nil ] 				on: Error				do: [ :ex | dstString ].	^self displayPatchFrom: formattedSrcString to: formattedDstString tryWords: aBoolean! !!FileContentsBrowser methodsFor: 'diffs' stamp: 'jmv 2/12/2010 16:05'!methodDiffFor: aString class: aPseudoClass selector: selector meta: meta 	"Answer the diff between the current copy of the given class/selector/meta for the string provided"	| theClass source |	theClass _ Smalltalk				at: aPseudoClass name				ifAbsent: [^ aString copy].	meta		ifTrue: [theClass _ theClass class].	(theClass includesSelector: selector)		ifFalse: [^ aString copy].	source _ theClass sourceCodeAt: selector.	^ Cursor wait		showWhile: [			DifferenceFinder				displayPatchFrom: source to: aString				tryWords: self shouldDiffWords				prettyPrintedIn: (self showingAnyKindOfPrettyDiffs ifTrue: [theClass])]! !!FileContentsBrowser methodsFor: 'diffs' stamp: 'jmv 2/12/2010 15:53'!modifiedClassDefinition	| pClass rClass old new |	pClass := self selectedClassOrMetaClass.	pClass hasDefinition ifFalse:[^pClass definition].	rClass := Smalltalk at: self selectedClass name asSymbol ifAbsent:[nil].	rClass isNil ifTrue:[^pClass definition].	self metaClassIndicated ifTrue:[ rClass := rClass class].	old := rClass definition.	new := pClass definition.	^Cursor wait showWhile:[		DifferenceFinder displayPatchFrom: old to: new tryWords: true ]! !!TextEditor methodsFor: 'menu messages' stamp: 'jmv 2/12/2010 15:53'!compareToClipboard	"Check to see if whether the receiver's text is the same as the text currently on the clipboard, and inform the user."	| s1 s2 |	s1 _ self clipboardText string.	s2 _ paragraph text string.	s1 = s2 ifTrue: [^ self inform: 'Exact match'].	(StringHolder new textContents:		(DifferenceFinder displayPatchFrom: s1 to: s2 tryWords: true))			openLabel: 'Comparison to Clipboard Text'! !!VersionsBrowser methodsFor: 'menu' stamp: 'jmv 2/12/2010 16:06'!compareToOtherVersion	"Prompt the user for a reference version, then spawn a window 	showing the diffs between the older and the newer of the current 	version and the reference version as text."	| change1 change2 s1 s2 differDesc diffWords |	change1 := changeList at: listIndex ifAbsent: [ ^self ].	change2 := ((SelectionMenu				labels: (list copyWithoutIndex: listIndex)				selections: (changeList copyWithoutIndex: listIndex)) startUp) ifNil: [ ^self ].		"compare earlier -> later"	"change1 timeStamp < change2 timeStamp		ifFalse: [ | temp | temp _ change1. change1 _ change2. change2 _ temp ]."	s1 := change1 string.	s2 := change2 string.	s1 = s2		ifTrue: [^ self inform: 'Exact Match' translated].	diffWords _ self shouldDiffWords.	differDesc _ diffWords		ifTrue: [ 'Words']		ifFalse: [ 'Lines'].	(StringHolder new		textContents: (DifferenceFinder			displayPatchFrom: s1 to: s2			tryWords: diffWords			prettyPrintedIn: (self showingAnyKindOfPrettyDiffs ifTrue: [classOfMethod])))		openLabel: 			(('Comparison from {1} to {2}: ', differDesc, 				(self showingAnyKindOfPrettyDiffs ifTrue: [', using prettyPrint'] ifFalse: [''])) 					format: { change1 stamp. change2 stamp })! !!ClassCommentVersionsBrowser methodsFor: 'menu' stamp: 'jmv 2/12/2010 16:05'!compareToCurrentVersion	"If the current selection corresponds to a method in the system, then spawn a window showing the diffs as text"	| change s1 s2 differDesc diffWords |	listIndex = 0		ifTrue: [^ self].	change _ changeList at: listIndex.	s1 _ classOfMethod organization classComment.	s2 _ change string.	s1 = s2		ifTrue: [^ self inform: 'Exact Match'].	diffWords _ self shouldDiffWords.	differDesc _ diffWords		ifTrue: [ 'Words']		ifFalse: [ 'Lines'].	(StringHolder new		textContents: (DifferenceFinder			displayPatchFrom: s1 to: s2			tryWords: diffWords))				openLabel: 'Comparison to Current Version: ', differDesc! !!ClassCommentVersionsBrowser methodsFor: 'basic function' stamp: 'jmv 2/12/2010 16:05'!diffedVersionContents	"Answer diffed version contents, maybe pretty maybe not"	| change class earlier later |	(listIndex = 0			or: [changeList size < listIndex])		ifTrue: [^ ''].	change _ changeList at: listIndex.	later _ change text.	class _ self selectedClass.	(listIndex = changeList size or: [class == nil])		ifTrue: [^ later].	earlier _ (changeList at: listIndex + 1) text.	^DifferenceFinder		displayPatchFrom: earlier to: later		tryWords: self shouldDiffWords! !!ClassCommentVersionsBrowser methodsFor: 'misc' stamp: 'jmv 2/12/2010 16:14'!contentsSymbolQuints	"Answer a list of quintuplets representing information on the alternative views available in the code pane"	^ #((source		togglePlainSource 		showingPlainSourceString		'source'	'the textual source code as writen')(lineDiffs		toggleLineDiffing		showingLineDiffsString	'showDiffs'	'the textual source diffed from its prior version')	)! !DifferenceFinder class removeSelector: #buildDisplayPatchFrom:to:!DifferenceFinder class removeSelector: #buildDisplayPatchFrom:to:inClass:prettyDiffs:!DifferenceFinder class removeSelector: #buildDisplayPatchFrom:to:prettyPrintedInClass:!DifferenceFinder class removeSelector: #displayPatchFrom:to:compareWords:!DifferenceFinder class removeSelector: #displayPatchFrom:to:compareWords:asPrettyPrintedMethodsInClass:!DifferenceFinder removeSelector: #buildDisplayPatch!DifferenceFinder removeSelector: #sourceCodeDifferences!CodeHolder removeSelector: #prettyDiffButton!CodeHolder removeSelector: #prettyDiffButton2!CodeHolder removeSelector: #regularDiffButton!CodeHolder removeSelector: #regularDiffButton2!CodeHolder removeSelector: #shouldUseAlternativeDiffer!CodeHolder removeSelector: #showPrettyDiffs2:!CodeHolder removeSelector: #showPrettyDiffs:!CodeHolder removeSelector: #showRegularDiffs2:!CodeHolder removeSelector: #showRegularDiffs:!CodeHolder removeSelector: #showingPrettyDiffs!CodeHolder removeSelector: #showingPrettyDiffs2!CodeHolder removeSelector: #showingPrettyDiffsString!CodeHolder removeSelector: #showingPrettyDiffsString2!CodeHolder removeSelector: #showingRegularDiffs!CodeHolder removeSelector: #showingRegularDiffs2!CodeHolder removeSelector: #showingRegularDiffsString!CodeHolder removeSelector: #showingRegularDiffsString2!CodeHolder removeSelector: #togglePrettyDiffing!CodeHolder removeSelector: #togglePrettyDiffing2!CodeHolder removeSelector: #toggleRegularDiffing!CodeHolder removeSelector: #toggleRegularDiffing2!Smalltalk removeClassNamed: #ClassDiffBuilder!Smalltalk removeClassNamed: #DiffElement!Smalltalk removeClassNamed: #PrettyTextDiffBuilder!Smalltalk removeClassNamed: #TextDiffBuilder!