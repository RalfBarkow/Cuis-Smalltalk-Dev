'From Cuis 4.0 of 16 November 2011 [latest update: #1144] on 10 December 2011 at 1:48:56 pm'!!TextEditor methodsFor: 'undo & redo' stamp: 'jmv 12/10/2011 13:48'!                            offerUndoHistory	| index labels current |	current _ model undoRedoCommandsPosition.	labels _ model undoRedoCommands collectWithIndex: [ :each :i | 		(i = current ifTrue: [ '<on>' ] ifFalse: [ '<off>' ]), each printString ].	labels isEmpty ifFalse: [		index _ (PopUpMenu			labelArray: labels			lines: #()) startUp.		index = current ifTrue: [ ^self ].		index = 0 ifTrue: [ ^self ].		index < current			ifTrue: [ current - index timesRepeat: [ self undo ]]			ifFalse: [ index - current timesRepeat: [ self redo ]]]! !