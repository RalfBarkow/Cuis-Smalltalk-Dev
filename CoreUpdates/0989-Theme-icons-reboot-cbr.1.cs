'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 18 May 2011 at 10:04:17 pm'!!Theme methodsFor: 'icons' stamp: 'cbr 5/16/2011 01:10'!acceptIcon			"	#( 'resolution' 'context' 'filename' )	"	^ self fetch: #( '16x16' 'smalltalk' 'accept' )! !!Theme methodsFor: 'icon lookup' stamp: 'cbr 5/16/2011 08:05'!fetch: aTuple "	#( 'resolution' 'context' 'filename' )	"	"Get an icon from Content. See icons protocol."	| contentSpecifier icon themeGuess |		icon _ nil.	themeGuess _ self class.	contentSpecifier _ self appendExtensionToContentSpec: aTuple.	[ icon isNil ] 		whileTrue: [			icon _ self class content 				get: (self prepend: themeGuess toContentSpec: contentSpecifier).				icon ifNotNil: [ ^ icon ].				themeGuess = Theme				ifTrue: [ ^ nil "See comment in ContentPack>>get: --cbr" ].				themeGuess _ themeGuess superclass		]! !Theme removeSelector: #acceptIconX!Theme removeSelector: #addressBookIconX!Theme removeSelector: #appearanceIconX!Theme removeSelector: #blankIconX!Theme removeSelector: #cancelIconX!Theme removeSelector: #changesIconX!Theme removeSelector: #chatIconX!Theme removeSelector: #classIconX!Theme removeSelector: #clockIconX!Theme removeSelector: #closeIconX!Theme removeSelector: #closeMenuIconX!Theme removeSelector: #collapseIconX!Theme removeSelector: #copyIconX!Theme removeSelector: #cutIconX!Theme removeSelector: #dateIconX!Theme removeSelector: #debugIconX!Theme removeSelector: #deleteIconX!Theme removeSelector: #developmentIconX!Theme removeSelector: #displayIconX!Theme removeSelector: #doItIconX!Theme removeSelector: #editFindReplaceIconX!Theme removeSelector: #emblemImportantIconX!Theme removeSelector: #exitFullscreenIconX!Theme removeSelector: #expandIconX!Theme removeSelector: #exploreIconX!Theme removeSelector: #fileOutIconX!Theme removeSelector: #findIconX!Theme removeSelector: #fontXGenericIconX!Theme removeSelector: #formatJustifyCenterIconX!Theme removeSelector: #formatJustifyFillIconX!Theme removeSelector: #formatJustifyLeftIconX!Theme removeSelector: #formatJustifyRightIconX!