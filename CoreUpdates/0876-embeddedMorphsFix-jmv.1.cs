'From Cuis 3.1 of 4 March 2011 [latest update: #850] on 17 March 2011 at 3:58:49 pm'!!CharacterScanner methodsFor: 'scanning' stamp: 'jmv 3/17/2011 14:09'!placeEmbeddedObject: anchoredFormOrMorph	"Place the anchoredMorph or return false if it cannot be placed.	In any event, advance destX by its width."	"Workaround: The following should really use #textAnchorType"	anchoredFormOrMorph relativeTextAnchorPosition ifNotNil: [^true].	destX _ destX + anchoredFormOrMorph width.	(destX > rightMargin and: [ lastIndex ~= line first ])		"Won't fit, but  not at start of a line. Start a new line with it"		ifTrue: [ ^ false].	lastIndex _ lastIndex + 1.	^ true! !!CompositionScanner methodsFor: 'stop conditions' stamp: 'jmv 3/17/2011 14:09'!placeEmbeddedObject: anchoredFormOrMorph	| descent |	"Workaround: The following should really use #textAnchorType"	anchoredFormOrMorph relativeTextAnchorPosition ifNotNil: [ ^true ].	(super placeEmbeddedObject: anchoredFormOrMorph) ifFalse: [		line stop: lastIndex-1.		^ false].	descent _ lineHeight - baseline.	baseline _ baseline max: anchoredFormOrMorph height.	lineHeight _ lineHeight + descent.	line stop: lastIndex.	^ true! !