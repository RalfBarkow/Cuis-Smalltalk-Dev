'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 26 May 2011 at 8:28:11 am'!!SpaceTally commentStamp: '<historical>' prior: 0!I'm responsible to help getting information about system space usage. The information I compute is represented by a spaceTallyItemtry something like: ((SpaceTally new spaceTally: (Array with: TextMorph with: Point)) 	asSortedCollection: [:a :b | a spaceForInstances > b spaceForInstances]) SpaceTally new systemWideSpaceTallyThis class has been created from a part of SystemDictionary. It still deserves a niceclean, such as using object instead of array having 4 slots.sd-20 June 2003Also try:| stream |stream _ FileStream forceNewFileNamed: 'MemoryAnalysis.txt'.[ SpaceTally new printSpaceAnalysis: 1 on: stream ]	ensure: [ stream close ]!!Object methodsFor: 'printing' stamp: 'jmv 5/25/2011 15:06'!nominallyUnsent: aSelectorSymbol	"From within the body of a method which is not formally sent within the system, but which you intend to have remain in the system (for potential manual invocation, or for documentation, or perhaps because it's sent by commented-out-code that you anticipate uncommenting out someday, send this message, with the selector itself as the argument.This will serve two purposes:	(1)  The method will not be returned by searches for unsent selectors (because it, in a manner of speaking, sends itself).	(2)	You can locate all such methods by browsing senders of #nominallyUnsent:"	"(jmv) Correction: send the unsent symbol from elsewhere. If it is sent from whithin itself, it will still appear as unsent."	false ifTrue: [self flag: #nominallyUnsent:]    "So that this method itself will appear to be sent"! !!SpaceTally methodsFor: 'fileOut' stamp: 'jmv 5/25/2011 17:07'!printSpaceAnalysis: threshold on: aStream	"SpaceTally new printSpaceAnalysis: 1 on:(FileStream forceNewFileNamed: 'STspace.text')"	"sd-This method should be rewrote to be more coherent within the rest of the class 	ie using preAllocate and spaceForInstanceOf:"	"If threshold > 0, then only those classes with more than that number	of instances will be shown, and they will be sorted by total instance space.	If threshold = 0, then all classes will appear, sorted by name."	| codeSpace instCount instSpace totalCodeSpace totalInstCount totalInstSpace eltSize n totalPercent percent |	Smalltalk garbageCollect.	totalCodeSpace _ totalInstCount _ totalInstSpace _ n _ 0.	results _ OrderedCollection new: Smalltalk classNames size.'Taking statistics...'	displayProgressAt: Sensor mousePoint	from: 0 to: Smalltalk classNames size	during: [:bar |	Smalltalk allClassesDo:		[:cl | codeSpace _ cl spaceUsed.		bar value: (n _ n+1).		Smalltalk garbageCollectMost.		instCount _ cl instanceCount.		instSpace _ (cl indexIfCompact > 0 ifTrue: [4] ifFalse: [8])*instCount. "Object headers"		cl isVariable			ifTrue: [eltSize _ cl isBytes ifTrue: [1] ifFalse: [4].					cl allInstancesDo: [:x | 						x == Display bits ifFalse: [							instSpace _ instSpace + (x basicSize*eltSize)]]]			ifFalse: [instSpace _ instSpace + (cl instSize*instCount*4)].		results add: (SpaceTallyItem analyzedClassName: cl name codeSize: codeSpace instanceCount:  instCount spaceForInstances: instSpace).		totalCodeSpace _ totalCodeSpace + codeSpace.		totalInstCount _ totalInstCount + instCount.		totalInstSpace _ totalInstSpace + instSpace]].	totalPercent _ 0.0.	aStream timeStamp.	aStream		nextPutAll: ('Class' padded: #right to: 30 with: $ );		nextPutAll: ('code space' padded: #left to: 12 with: $ );		nextPutAll: ('# instances' padded: #left to: 12 with: $ );		nextPutAll: ('inst space' padded: #left to: 12 with: $ );		nextPutAll: ('percent' padded: #left to: 8 with: $ ); cr.	threshold > 0 ifTrue: [		"If inst count threshold > 0, then sort by space"		results _ (results select: [:s | s instanceCount >= threshold or: [s spaceForInstances > (totalInstSpace // 500)]])			asArray sort: [:s :s2 | s spaceForInstances > s2 spaceForInstances]].	results do: [:s |		aStream			nextPutAll: (s analyzedClassName padded: #right to: 30 with: $ );			nextPutAll: (s codeSize printString padded: #left to: 12 with: $ );			nextPutAll: (s instanceCount printString padded: #left to: 12 with: $ );			nextPutAll: (s spaceForInstances printString padded: #left to: 14 with: $ ).		percent _ s spaceForInstances*100.0/totalInstSpace roundTo: 0.1.		totalPercent _ totalPercent + percent.		percent >= 0.1 ifTrue: [			aStream nextPutAll: (percent printString padded: #left to: 8 with: $ )].		aStream cr].	aStream		cr; nextPutAll: ('Total' padded: #right to: 30 with: $ );		nextPutAll: (totalCodeSpace printString padded: #left to: 12 with: $ );		nextPutAll: (totalInstCount printString padded: #left to: 12 with: $ );		nextPutAll: (totalInstSpace printString padded: #left to: 14 with: $ );		nextPutAll: ((totalPercent roundTo: 0.1) printString padded: #left to: 8 with: $ ).! !!StrikeFont class methodsFor: 'removing' stamp: 'jmv 5/25/2011 17:38'!removeMostFonts"StrikeFont removeMostFonts"	| familyDict |	familyDict _ AvailableFonts at: 'DejaVu'.	familyDict keys do: [ :k |		(#(8 10 12) includes: k) 			ifTrue: [				(familyDict at: k) derivativeFont: nil at: 0 ]			ifFalse: [				familyDict removeKey: k ]].		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 10)		(setListFontTo: 'DejaVu' 10)		(setMenuFontTo: 'DejaVu' 10)		(setWindowTitleFontTo: 'DejaVu' 12)		(setBalloonHelpFontTo: 'DejaVu' 8)		(setCodeFontTo: 'DejaVu' 10)		(setButtonFontTo: 'DejaVu' 10))! !!SystemDictionary methodsFor: 'shrinking' stamp: 'jmv 5/25/2011 16:54'!presumedSentMessages	| sent |	"	In addition to those here, if it is desired to preserve some methods from deletion, see #nominallyUnsent:	Smalltalk presumedSentMessages	"	"The following should be preserved for doIts, etc"	sent _ IdentitySet new.	#( rehashWithoutBecome compactSymbolTable		browseAllSelect:  lastRemoval		vScrollBarValue: hScrollBarValue: 		to: removeClassNamed:		dragon: hilberts: mandala: web test3 factorial tinyBenchmarks benchFib		newDepth: restoreAfter: forgetDoIts zapAllMethods obsoleteClasses		removeAllUnSentMessages abandonSources removeUnreferencedKeys		zapOrganization condenseChanges browseObsoleteReferences		subclass:instanceVariableNames:classVariableNames:poolDictionaries:category:		methodsFor:stamp: methodsFor:stamp:prior: instanceVariableNames:		startTimerEventLoop unusedClasses		unimplemented		reduceCuis		variableSubclass:instanceVariableNames:classVariableNames:poolDictionaries:category:		variableByteSubclass:instanceVariableNames:classVariableNames:poolDictionaries:category:		variableWordSubclass:instanceVariableNames:classVariableNames:poolDictionaries:category:		weakSubclass:instanceVariableNames:classVariableNames:poolDictionaries:category:		printSpaceAnalysis:on:) do: [ :sel |			sent add: sel].	"The following may be sent by perform: in dispatchOnChar..."	(TextEditor cmdActions) asSet do: [ :sel | sent add: sel].	(SmalltalkEditor cmdActions) asSet do: [ :sel | sent add: sel].	#(beReadOnlyBinding beReadWriteBinding changeSetCategoryClass belongsInAll:) do: [ :sel |		sent add: sel].	^ sent! !!SystemDictionary methodsFor: 'shrinking' stamp: 'jmv 5/25/2011 17:37'!reduceCuis	"	Smalltalk reduceCuis	"	| cs keep n unused newDicts oldDicts |	self nominallyUnsent: #reduceCuis.		"Remove icons"	ClassicTheme beCurrent.	World backgroundImageData: nil.	Preferences useNoIcons.	Theme current initialize.	Theme content: nil.	Color shutDown.	FormCanvas clearFormsCache.	Transcript clear.	Clipboard default initialize.		Smalltalk removeClassNamed: #ColorPickerMorph.	Smalltalk removeClassNamed: #SketchMorph.	"Remove some methods, even if they have senders.""	ColorPickerMorph class removeSelector: #buildEyedropperIcon."	CursorWithAlpha class removeSelector: #buildBiggerNormal."	SketchMorph class removeSelector: #buildPaintingIcon."	Theme removeSelector: #miscellaneousIcons.	Utilities removeSelector: #vmStatisticsReportString.	SystemDictionary removeSelector: #recreateSpecialObjectsArray.	World submorphsDo: [ :a | a delete ].	StrikeFont removeMostFonts.	StrikeFont saveSpace.	Smalltalk garbageCollect.	"????	Smalltalk organization removeCategoriesMatching: 'Signal Processing*'.	SystemOrganization removeSystemCategory: 'LinearAlgebra'.	Smalltalk organization removeCategoriesMatching: 'Sound-*'	"	Beeper setDefault: nil.	Smalltalk removeEmptyMessageCategories.	Smalltalk organization removeEmptyCategories.	keep := OrderedCollection new.	keep addAll: #(ZipConstants GZipConstants ZipFileConstants ChronologyConstants SpaceTally).	unused := Smalltalk unusedClasses copyWithoutAll: keep.	[		#hereWeGo print.		unused do: [:c | 			c print.			(Smalltalk at: c) removeFromSystem]. 		n := Smalltalk removeAllUnSentMessages.		unused := Smalltalk unusedClasses copyWithoutAll: keep.		n > 0 or: [ 			unused notEmpty ]] whileTrue.	cs _ ChangeSorter assuredChangeSetNamed: 'Unnamed1'.	ChangeSet newChanges: cs.	ChangeSet current clear.	ChangeSorter allChangeSets copy do: [ :cs2 |  cs2 == cs ifFalse: [ ChangeSorter removeChangeSet: cs2 ]].	Smalltalk garbageCollect.	Smalltalk organization removeEmptyCategories.	Symbol rehash.	"Shrink method dictionaries."	Smalltalk garbageCollect.	oldDicts _ MethodDictionary allInstances.	newDicts _ Array new: oldDicts size.	oldDicts withIndexDo: [:d :index | 		newDicts at: index put: d rehashWithoutBecome ].	oldDicts elementsExchangeIdentityWith: newDicts.	oldDicts _ newDicts _ nil.   "Sanity checks""   Undeclared   Smalltalk cleanOutUndeclared   Smalltalk browseUndeclaredReferences   Smalltalk obsoleteClasses   Smalltalk obsoleteBehaviors    Smalltalk browseObsoleteMethodReferences   SmalltalkImage current fixObsoleteReferences   Smalltalk browseAllUnimplementedCalls"! !