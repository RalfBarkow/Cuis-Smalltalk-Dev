'From Cuis 3.3 of 2 June 2011 [latest update: #1024] on 11 October 2011 at 7:47:53 pm'!!classDefinition: #PluggableListMorph category: #'Morphic-Views for Models'!ScrollPane subclass: #PluggableListMorph	instanceVariableNames: 'list getListSelector getListSizeSelector getIndexSelector setIndexSelector keystrokeActionSelector autoDeselect lastKeystrokeTime lastKeystrokes doubleClickSelector handlesBasicKeys listMorph menuGetter mainView '	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!PluggableListMorph methodsFor: 'event handling' stamp: 'jmv 10/11/2011 19:47'!keyStroke: event 	"Process keys"		| aCharacter |	(self focusKeyboardFor: event)		ifTrue: [ ^ self ].	(self scrollByKeyboard: event) 		ifTrue: [ ^self ].	aCharacter _ event keyCharacter.	(self arrowKey: aCharacter)		ifTrue: [ ^self ].	aCharacter asciiValue = 27 ifTrue: [	" escape key"		^ self mouseButton2Activity].	event anyModifierKeyPressed		ifTrue: [			(self keystrokeAction: aCharacter)				ifTrue: [ ^self ]].	^ self keyboardSearch: aCharacter! !PluggableListMorph removeSelector: #handlesBasicKeys!!classDefinition: #PluggableListMorph category: #'Morphic-Views for Models'!ScrollPane subclass: #PluggableListMorph	instanceVariableNames: 'list getListSelector getListSizeSelector getIndexSelector setIndexSelector keystrokeActionSelector autoDeselect lastKeystrokeTime lastKeystrokes doubleClickSelector listMorph menuGetter mainView'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!