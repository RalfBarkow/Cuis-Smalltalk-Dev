'From Cuis 3.3 of 2 June 2011 [latest update: #1024] on 1 November 2011 at 9:59:05 am'!!Editor methodsFor: 'menu commands' stamp: 'jmv 11/1/2011 09:13'!offerMenuFromEsc: aKeyboardEvent	"The escape key was hit while the receiver has the keyboard focus; take action"	^ ActiveEvent shiftPressed		ifFalse: [			self openMenu]! !!Editor methodsFor: 'menu commands' stamp: 'jmv 11/1/2011 09:14'!openMenu	(morph respondsTo: #editView)		ifTrue: [morph editView mouseButton2Activity].	^ true! !!TextEditorTest methodsFor: 'as yet unclassified' stamp: 'jmv 11/1/2011 09:24'!testSimpleEditor	"	TextEditorTest new testSimpleEditor	"	| editor |	editor _ SimpleEditor new.	self shouldnt: [ editor offerMenuFromEsc: nil ] raise: Exception! !!TextModel methodsFor: 'object serialization' stamp: 'jmv 11/1/2011 09:40'!convertToCurrentVersion: varDict refStream: smartRefStrm	"Maybe old instances won't have this variable set."	undoRedoCommands ifNil: [		undoRedoCommands _ ReadWriteStream on: Array new ]! !TextEditor removeSelector: #offerMenuFromEsc:!TextEditor removeSelector: #openMenu!