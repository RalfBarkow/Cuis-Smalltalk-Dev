'From Cuis 2.9 of 5 November 2010 [latest update: #634] on 16 December 2010 at 6:44:39 pm'!!classDefinition: #HandMorph category: #'Morphic-Kernel'!Morph subclass: #HandMorph	instanceVariableNames: 'mouseFocus keyboardFocus eventListeners mouseListeners keyboardListeners mouseClickState mouseOverHandler lastMouseEvent targetOffset damageRecorder temporaryCursor temporaryCursorOffset hasChanged savedPatch userInitials lastEventBuffer lastKeyDownValue '	classVariableNames: 'DoubleClickTime EventStats NewEventRules NormalCursor PasteBuffer ShowEvents '	poolDictionaries: 'EventSensorConstants'	category: 'Morphic-Kernel'!!HandMorph methodsFor: 'private events' stamp: 'jmv 12/16/2010 17:35'!aagenerateKeyboardEvent: evtBuf 	"Generate the appropriate mouse event for the given raw event buffer"	| buttons modifiers type keyValue pressType stamp |	stamp _ evtBuf second.	stamp = 0 ifTrue: [stamp _ Time millisecondClockValue].	keyValue _ evtBuf sixth > 0		ifTrue: [ (Character iso8859s15CodeForUnicodeCodePoint: evtBuf sixth)			ifNil: [ Character macRomanToLatin1: evtBuf third] ]		ifFalse: [ Character macRomanToLatin1: evtBuf third ].	modifiers _ evtBuf fifth.	pressType _ evtBuf fourth.	pressType = EventKeyDown ifTrue: [		type _ #keyDown.		lastKeyDownValue _ keyValue].	pressType = EventKeyUp ifTrue: [type _ #keyUp].	pressType = EventKeyChar ifTrue: [		type _ #keystroke.		"If Control key pressed, and the VM answers a code below 27,		 it means it did the translation, convert it back to regular character:		We want to handle the meaning of ctrl ourselves."		(modifiers anyMask: 2) ifTrue: [		"Control key pressed"			keyValue < 27 ifTrue: [								"But we don't want to do it for Home/End/PgUp/PgDn, just for alphabetic keys"				lastKeyDownValue = keyValue ifFalse: [		"If equal, real Home/End/PgUp/PgDn in Windows => don't translate"					(keyValue + 64 = lastKeyDownValue or: [ 	"If Equal, Ctrl-alphabetic in Windows => do translate"							lastKeyDownValue < 64 ]) ifTrue: [		"Not on windows. If less (not sure about the bound), alphabetic on Mac => do translate"						keyValue _ (modifiers anyMask: 1)							ifFalse: [ keyValue + 96 ]	"shift not pressed: conver to lowercase letter"							ifTrue: [ keyValue + 64 ]].	"shift pressed: conver to uppercase letter"					]				].			"Act as if command/alt was pressed for some usual Windows ctrl-key combinations"			(self shouldControlEmulateAltFor: keyValue) ifTrue: [				Display fill: (10@10 extent: 30@30) fillColor: Color random.				modifiers _ modifiers bitOr: 8 ]			]].	buttons _ modifiers bitShift: 3.	^KeyboardEvent new 		setType: type		buttons: buttons		position: self position		keyValue: keyValue		hand: self		stamp: stamp! !!HandMorph methodsFor: 'private events' stamp: 'jmv 12/16/2010 18:41'!generateKeyboardEvent: evtBuf 	"Generate the appropriate mouse event for the given raw event buffer"	| buttons modifiers type keyValue pressType stamp |	stamp _ evtBuf second.	stamp = 0 ifTrue: [stamp _ Time millisecondClockValue].	(evtBuf sixth <= 0 or: [		(keyValue _ (Character iso8859s15CodeForUnicodeCodePoint: evtBuf sixth)) isNil ])			ifTrue: [ keyValue _ Character macRomanToLatin1: evtBuf third ].	modifiers _ evtBuf fifth.	pressType _ evtBuf fourth.	pressType = EventKeyDown ifTrue: [		type _ #keyDown.		lastKeyDownValue _ keyValue].	pressType = EventKeyUp ifTrue: [type _ #keyUp].	pressType = EventKeyChar ifTrue: [		type _ #keystroke.		"If Control key pressed, and the VM answers a code below 27,		 it means it did the translation, convert it back to regular character:		We want to handle the meaning of ctrl ourselves."		(modifiers anyMask: 2) ifTrue: [		"Control key pressed"			keyValue < 27 ifTrue: [								"But we don't want to do it for Home/End/PgUp/PgDn, just for alphabetic keys"				lastKeyDownValue = keyValue ifFalse: [		"If equal, real Home/End/PgUp/PgDn in Windows => don't translate"					(keyValue + 64 = lastKeyDownValue or: [ 	"If Equal, Ctrl-alphabetic in Windows => do translate"							lastKeyDownValue < 64 ]) ifTrue: [		"Not on windows. If less (not sure about the bound), alphabetic on Mac => do translate"						keyValue _ (modifiers anyMask: 1)							ifFalse: [ keyValue + 96 ]	"shift not pressed: conver to lowercase letter"							ifTrue: [ keyValue + 64 ]].	"shift pressed: conver to uppercase letter"					]				].			"Act as if command/alt was pressed for some usual Windows ctrl-key combinations"			(self shouldControlEmulateAltFor: keyValue) ifTrue: [				modifiers _ modifiers bitOr: 8 ]			]].	buttons _ modifiers bitShift: 3.	^KeyboardEvent new 		setType: type		buttons: buttons		position: self position		keyValue: keyValue		hand: self		stamp: stamp! !!HandMorph methodsFor: 'private events' stamp: 'jmv 12/16/2010 18:31'!shouldControlEmulateAltFor: keyValue	"Answer a list of key letters that are used for common editing operations	on different platforms."	"	^{ $c . $x . $v . $a . $s . $f . $g . $z } collect: [ :a | a asciiValue ]	" 	^#(99 120 118 97 115 102 103 122) includes: keyValue! !!TextEditor methodsFor: 'typing/selecting keys' stamp: 'jmv 12/16/2010 16:06'!forwardDelete: aKeyboardEvent	"Delete forward over the next character.	  Make Undo work on the whole type-in, not just the one char.	wod 11/3/1998: If there was a selection use #zapSelectionWith: rather than #backspace: which was 'one off' in deleting the selection. Handling of things like undo or typeIn area were not fully considered."	| startIndex usel upara uinterval ind stopIndex |	startIndex _ self markIndex.	startIndex > paragraph text size ifTrue: [		^ false].	self hasSelection ifTrue: [		"there was a selection"		self zapSelectionWith: self nullText.		^ false].	"Null selection - do the delete forward"	beginTypeInBlock ifNil: [	"no previous typing.  openTypeIn"		self openTypeIn. UndoSelection _ self nullText].	uinterval _ UndoInterval copy.	upara _ UndoParagraph copy.	stopIndex := startIndex.	(aKeyboardEvent keyValue = 127 and: [ aKeyboardEvent shiftPressed ])		ifTrue: [stopIndex := (self nextWordStart: stopIndex) - 1].	self selectFrom: startIndex to: stopIndex.	self replaceSelectionWith: self nullText.	self selectFrom: startIndex to: startIndex-1.	UndoParagraph _ upara.  UndoInterval _ uinterval.	UndoMessage selector == #noUndoer ifTrue: [		(UndoSelection is: #Text) ifTrue: [			usel _ UndoSelection.			ind _ startIndex. "UndoInterval startIndex"			usel replaceFrom: usel size + 1 to: usel size with:				(UndoParagraph text copyFrom: ind to: ind).			UndoParagraph text replaceFrom: ind to: ind with:self nullText]].	^false! !!classDefinition: #HandMorph category: #'Morphic-Kernel'!Morph subclass: #HandMorph	instanceVariableNames: 'mouseFocus keyboardFocus eventListeners mouseListeners keyboardListeners mouseClickState mouseOverHandler lastMouseEvent targetOffset damageRecorder temporaryCursor temporaryCursorOffset hasChanged savedPatch userInitials lastEventBuffer lastKeyDownValue'	classVariableNames: 'DoubleClickTime EventStats NewEventRules NormalCursor PasteBuffer ShowEvents'	poolDictionaries: 'EventSensorConstants'	category: 'Morphic-Kernel'!