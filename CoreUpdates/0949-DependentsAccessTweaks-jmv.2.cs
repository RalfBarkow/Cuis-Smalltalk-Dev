'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 25 April 2011 at 6:02:22 pm'!!Object methodsFor: 'dependents access' stamp: 'jmv 4/25/2011 17:48'!canDiscardEdits	"Answer true if none of the views on this model has unaccepted edits that matter."	"This message is sent to models, but it is views who actually know.	Look for a dependent (i.e. a kind of view) that answers false"	self dependents		do: [:each | each canDiscardEdits ifFalse: [^ false]]		without: self.	^ true! !!Object methodsFor: 'dependents access' stamp: 'jmv 4/25/2011 17:50'!hasUnacceptedEdits	"Answer true if any of the views on this object has unaccepted edits."	"This message is sent to models, but it is views who actually know.	Look for a dependent (i.e. a kind of view) that answers true"	self dependents		do: [:each | each hasUnacceptedEdits ifTrue: [^ true]]		without: self.	^ false! !!Object methodsFor: 'tracing' stamp: 'jmv 4/25/2011 17:57'!explorePointers	ObjectExplorerWindow		open: (PointerExplorer new rootObject: self)		label: 'References to ', self printString! !!Model methodsFor: 'dependents' stamp: 'jmv 4/25/2011 17:48'!canDiscardEdits	"Answer true if none of the views on this model has unaccepted edits that matter."	"This message is sent to models, but it is views who actually know.	Look for a dependent (i.e. a kind of view) that answers false"	dependents ifNil: [^ true].	^ super canDiscardEdits! !!Model methodsFor: 'dependents' stamp: 'jmv 4/25/2011 17:50'!hasUnacceptedEdits	"Answer true if any of the views on this model has unaccepted edits."	"This message is sent to models, but it is views who actually know.	Look for a dependent (i.e. a kind of view) that answers true"	dependents ifNil: [^ false].	^ super hasUnacceptedEdits! !!Morph methodsFor: 'as yet unclassified' stamp: 'jmv 4/25/2011 17:50'!canDiscardEdits	"Answer true if none of the views on this model has unaccepted edits that matter."	"Views should not ask their dependents, even if there are any. Don't waste time with them."	^ true! !!Morph methodsFor: 'as yet unclassified' stamp: 'jmv 4/25/2011 17:51'!hasUnacceptedEdits	"Answer true if any of the views on this object has unaccepted edits."	"Views should not ask their dependents, even if there are any. Don't waste time with them."	^ false! !PointerExplorer removeSelector: #label!PointerExplorer removeSelector: #labelString!CodeWindow removeSelector: #hasUnacceptedEdits!