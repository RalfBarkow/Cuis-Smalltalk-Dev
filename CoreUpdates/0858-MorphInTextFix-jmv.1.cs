'From Cuis 3.1 of 4 March 2011 [latest update: #850] on 11 March 2011 at 3:35:51 pm'!!CompositionScanner methodsFor: 'stop conditions' stamp: 'jmv 3/11/2011 15:33'!placeEmbeddedObject: anchoredFormOrMorph	| descent |	"Workaround: The following should really use #textAnchorType"	anchoredFormOrMorph relativeTextAnchorPosition ifNotNil: [ ^true ].	(super placeEmbeddedObject: anchoredFormOrMorph) ifFalse: ["It doesn't fit"		"But if it's the first character then leave it here"		lastIndex < line first ifFalse:[			line stop: lastIndex-1.			^ false]].	descent _ lineHeight - baseline.	baseline _ baseline max: anchoredFormOrMorph height.	lineHeight _ lineHeight + descent.	line stop: lastIndex.	^ true! !