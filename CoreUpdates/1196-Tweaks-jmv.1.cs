'From Cuis 4.0 of 16 November 2011 [latest update: #1144] on 4 January 2012 at 3:24:27 pm'!!PluggableScrollPane methodsFor: 'scrolling' stamp: 'jmv 1/4/2012 11:35'!                         hideOrShowScrollBars	"Assume for a moment we don't need an horizontal scrollbar"	self hHideScrollBar.	"Add or remove vertical scrollbar, asuming for a monent there's no horizontal scrollbar,	to determine need of horizontal scrollbar..."	self vIsScrollbarNeeded		ifTrue: [ self vShowScrollBar ]		ifFalse: [ self vHideScrollBar ].	"If we need an horizontal scrollbar, add it."	self hIsScrollbarNeeded ifTrue: [		self hShowScrollBar.		"If horizontal scrollbar is needed, maybe vertical scrollbar will be needed too (even if we previously thoutht it wouldn't be needed)."			"Note that there is no chance of modifying the need of horizontal scrollbar: it was already needed. Therefore, there is no circularity here."		self vIsScrollbarNeeded  ifTrue: [			self vShowScrollBar ]].	self updateScrollBarsBounds! !!TextModel methodsFor: 'misc' stamp: 'jmv 1/4/2012 15:21'!          getSelection	"Answer the model's selection interval."	^ nil	"null selection"! !!PluggableTextModel methodsFor: 'misc' stamp: 'jmv 1/4/2012 15:19'!       getSelection	"Answer the model's selection interval."	^selectionGetter ifNotNil: [		textProvider perform: selectionGetter ]! !!TextModelMorph methodsFor: 'model access' stamp: 'jmv 1/4/2012 15:23'!                   setSelection: sel	sel == #all		ifTrue: [ self editor selectAll ]		ifFalse: [			sel				ifNil: [ self editor selectFrom:1 to: 0 ]				ifNotNil: [ self editor selectFrom: sel first to: sel last ]].	self scrollSelectionIntoView! !!TextProvider methodsFor: 'accessing' stamp: 'jmv 1/4/2012 15:20'!                 contentsSelection	"Return the interval of text in the code pane to select when I set the pane's contents"	^ nil  "null selection"! !!Browser methodsFor: 'accessing' stamp: 'jmv 1/4/2012 15:20'!                        contentsSelection	"Return the interval of text in the code pane to select when I set the pane's contents"	^(selectedMessageCategory notNil and: [ selectedMessage isNil ])		ifTrue: [ #all ]	"entire empty method template"		"or null selection"! !!MessageNames methodsFor: 'initialization' stamp: 'jmv 1/4/2012 15:20'!                            contentsSelection	"Return the interval of text in the search pane to select when I set the pane's contents"	^ #all 		"all of it"! !