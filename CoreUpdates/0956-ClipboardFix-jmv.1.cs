'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 27 April 2011 at 10:07:07 am'!!Clipboard methodsFor: 'accessing' stamp: 'jmv 4/27/2011 10:06'!retrieveObject	"Answer whatever was last stored in the clipboard"	| stringOrNil |	"If the OS clipboard has the id for our contents, or the same characters, then answer the richer Smalltalk object.	Note: if the (extended) clipboard contains a serialized object, it shouldn't contain an id, so	it is deserialized even if ivar contents contains the object. This is done to guarantee consistency with pasting	from another Cuis image."	stringOrNil _ self retrieveIdOrStringFromOS.	(stringOrNil = (self idFor: contents) or: [ stringOrNil = contents asString])		ifTrue: [			"We copy the object, because the result of each paste operation could be modified independently of the others afterwards			(and the same clipboard contents might be pasted many times)"			^contents copyForClipboard ].	"If we have the ExtendedClipboardInterface, try to get an RTF or Form"	Smalltalk at: #ExtendedClipboardInterface ifPresent: [ :clipboardInterface |		clipboardInterface current retrieveObject ifNotNil: [ :object | ^object ]].	"Otherwise answer the string brought by clipboard primitives,	but if they are not present or fail, use the internal clipboard."	^stringOrNil ifNil: [ contents copyForClipboard ]! !