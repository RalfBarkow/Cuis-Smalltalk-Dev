'From Cuis 2.9 of 5 November 2010 [latest update: #634] on 14 January 2011 at 9:15:27 am'!!Debugger methodsFor: 'accessing' stamp: 'jmv 1/14/2011 09:15'!acceptedStringOrText 	"Depending on the current selection, different information is retrieved.	Answer a string description of that information.  This information is the	method in the currently selected context."	^ self selectedContext			ifNotNil: [self selectedMessage]			ifNil: [String new]! !Debugger removeSelector: #contents!