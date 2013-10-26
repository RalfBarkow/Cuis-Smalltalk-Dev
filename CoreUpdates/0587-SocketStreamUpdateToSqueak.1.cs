'From Cuis 2.6 of 10 August 2010 [latest update: #540] on 30 August 2010 at 1:53:03 pm'!!SocketStream methodsFor: 'stream in' stamp: 'ar 2/23/2010 13:06'!next: n into: aCollection	"Read n objects into the given collection.	Return aCollection or a partial copy if less than	n elements have been read."	^self next: n into: aCollection startingAt: 1! !!SocketStream methodsFor: 'stream in' stamp: 'ar 2/23/2010 13:05'!next: anInteger into: aCollection startingAt: startIndex	"Read n objects into the given collection. 	Return aCollection or a partial copy if less than	n elements have been read."	"Implementation note: This method DOES signal timeout if not 	enough elements are received. It does NOT signal	ConnectionClosed as closing the connection is the only way by	which partial data can be read."	| start amount |	[self receiveData: anInteger] on: ConnectionClosed do:[:ex| ex return].	"Inlined version of nextInBuffer: to avoid copying the contents"	amount := anInteger min: (inNextToWrite - lastRead - 1).	start := lastRead + 1.	lastRead := lastRead + amount.	aCollection 		replaceFrom: startIndex 		to: startIndex + amount-1 		with: inBuffer 		startingAt: start.	^amount < anInteger 		ifTrue:[aCollection copyFrom: 1 to:  startIndex + amount-1]		ifFalse:[aCollection]! !!SocketStream methodsFor: 'stream in' stamp: 'ar 2/23/2010 13:06'!nextInto: aCollection	"Read the next elements of the receiver into aCollection.	Return aCollection or a partial copy if less than aCollection	size elements have been read."	^self next: aCollection size into: aCollection startingAt: 1.! !!SocketStream methodsFor: 'stream in' stamp: 'ar 2/23/2010 13:06'!nextInto: aCollection startingAt: startIndex	"Read the next elements of the receiver into aCollection.	Return aCollection or a partial copy if less than aCollection	size elements have been read."	^self next: (aCollection size - startIndex+1) into: aCollection startingAt: startIndex.! !!SocketStream methodsFor: 'stream in' stamp: 'gk 2/15/2005 14:16'!nextLineLf	| nextLine |	nextLine := self upToAll: String lf.	^nextLine! !!SocketStream methodsFor: 'stream in' stamp: 'ar 1/13/2010 22:30'!peek	"Return next byte, if inBuffer is empty	we recieve some more data and try again.	Do not consume the byte."	self atEnd ifTrue: [^nil].	self isInBufferEmpty ifTrue:		[self receiveData.		self atEnd ifTrue: [^nil]].	^inBuffer at: lastRead+1! !!SocketStream methodsFor: 'stream in' stamp: 'nice 3/16/2010 23:02'!readInto: aCollection startingAt: startIndex count: anInteger	"Read n objects into the given collection starting at startIndex. 	Return number of elements that have been read."	"Implementation note: This method DOES signal timeout if not 	enough elements are received. It does NOT signal	ConnectionClosed as closing the connection is the only way by	which partial data can be read."	| start amount |	[self receiveData: anInteger] on: ConnectionClosed do:[:ex| ex return].	"Inlined version of nextInBuffer: to avoid copying the contents"	amount := anInteger min: (inNextToWrite - lastRead - 1).	start := lastRead + 1.	lastRead := lastRead + amount.	aCollection 		replaceFrom: startIndex 		to: startIndex + amount-1 		with: inBuffer 		startingAt: start.	^amount! !!SocketStream methodsFor: 'stream in' stamp: 'ar 8/2/2010 18:50'!upTo: aCharacterOrByte	"Answer a subcollection from the current access position to the occurrence (if any, but not inclusive) of anObject in the receiver. If  anObject is not in the collection, answer the entire rest of the receiver."	"Note: The 100k limit below is compatible with the previous version though arguably incorrect. If you need unbounded behavior either up the argument or provide nil in which case we'll read until we get it or run out of memory"	^self upTo: aCharacterOrByte limit: 100000! !!SocketStream methodsFor: 'stream in' stamp: 'ar 8/22/2010 13:27'!upTo: aCharacterOrByte limit: nBytes	"Return data up to, but not including given character or byte. If the character is not in the stream, or not found within nBytes answer the available contents of the stream"	| target index result searchedSoFar |	"Deal with ascii vs. binary"	self isBinary 		ifTrue:[target := aCharacterOrByte asInteger] 		ifFalse:[target := aCharacterOrByte asCharacter].	"Look in the current inBuffer first"	index := inBuffer indexOf: target startingAt: lastRead + 1 ifAbsent:[0].	(index > 0 and: [(index + 1) <= inNextToWrite]) ifTrue: ["found it"		result := self nextInBuffer: index - lastRead - 1.		self skip: 1.		^ result	].	[searchedSoFar :=  self inBufferSize.	"Receive more data"	self receiveData.	"We only get recentlyRead = 0 in the case of a non-signaling socket close."	recentlyRead > 0] whileTrue:[		"Data begins at lastRead + 1, we add searchedSoFar as offset."		index := inBuffer indexOf: target						startingAt: (lastRead + searchedSoFar + 1)						ifAbsent:[0].		(index > 0 and: [(index + 1) <= inNextToWrite]) ifTrue: ["found it"			result := self nextInBuffer: index - lastRead - 1.			self skip: 1.			^ result		].		"Check if we've exceeded the max. amount"		(nBytes notNil and:[inNextToWrite - lastRead > nBytes]) 			ifTrue:[^self nextAllInBuffer].	].	"not found and (non-signaling) connection was closed"	^self nextAllInBuffer! !!SocketStream methodsFor: 'stream in' stamp: 'ar 8/2/2010 18:48'!upToAll: aStringOrByteArray	"Answer a subcollection from the current access position to the occurrence (if any, but not inclusive) of aCollection. If aCollection is not in the stream, answer the entire rest of the stream."	"Note: The 100k limit below is compatible with the previous version though arguably incorrect. If you need unbounded behavior either up the argument or provide nil in which case we'll read until we get it or run out of memory"	^self upToAll: aStringOrByteArray limit: 100000! !!SocketStream methodsFor: 'stream in' stamp: 'ar 8/22/2010 13:32'!upToAll: aStringOrByteArray limit: nBytes	"Answer a subcollection from the current access position to the occurrence (if any, but not inclusive) of aStringOrByteArray. If aCollection is not in the stream, or not found within nBytes answer the available contents of the stream"	| index sz result searchedSoFar target |	"Deal with ascii vs. binary"	self isBinary		ifTrue:[target := aStringOrByteArray asByteArray]		ifFalse:[target := aStringOrByteArray asString].	sz := target size.	"Look in the current inBuffer first"	index := inBuffer indexOfSubCollection: target						startingAt: lastRead - sz + 2.	(index > 0 and: [(index + sz) <= inNextToWrite]) ifTrue: ["found it"		result := self nextInBuffer: index - lastRead - 1.		self skip: sz.		^ result	].	[searchedSoFar :=  self inBufferSize.	"Receive more data"	self receiveData.	recentlyRead > 0] whileTrue:[		"Data begins at lastRead + 1, we add searchedSoFar as offset and 		backs up sz - 1 so that we can catch any borderline hits."		index := inBuffer indexOfSubCollection: target						startingAt: (lastRead + searchedSoFar - sz + 2 max: 1).		(index > 0 and: [(index + sz) <= inNextToWrite]) ifTrue: ["found it"			result := self nextInBuffer: index - lastRead - 1.			self skip: sz.			^ result		].		"Check if we've exceeded the max. amount"		(nBytes notNil and:[inNextToWrite - lastRead > nBytes]) 			ifTrue:[^self nextAllInBuffer].	].	"not found and (non-signaling) connection was closed"	^self nextAllInBuffer! !!SocketStream methodsFor: 'stream in' stamp: 'ar 8/5/2010 12:23'!upToEnd	"Answer all data coming in on the socket until the socket	is closed by the other end, or we get a timeout.	This means this method catches ConnectionClosed by itself.		NOTE: Does not honour timeouts if shouldSignal is false!!"	[[self isConnected] whileTrue: [self receiveData]]		on: ConnectionClosed		do: [:ex | "swallow it"]. 	^self nextAllInBuffer! !!SocketStream methodsFor: 'testing' stamp: 'ar 7/24/2010 14:50'!isDataAvailable	"Answer if more data can be read. It the inbuffer is empty, we check the socket for data. If it claims to have data available to read, we try to read some once and recursively call this method again. If something really was available it is now in the inBuffer. This is because there has been spurious dataAvailable when there really is no data to get.	Note: Some subclasses (such as SecureSocketStream) rely on the behavior here since even though data may be available in the underlying socket, it may not result in any output (yet)." 	self isInBufferEmpty ifFalse: [^true].	^socket dataAvailable		ifFalse: [false]		ifTrue: [self receiveAvailableData; isDataAvailable]! !!SocketStream methodsFor: 'initialize-release' stamp: 'ar 7/24/2010 15:13'!destroy	"Destroy the receiver and its underlying socket. Does not attempt to flush the output buffers. For a graceful close use SocketStream>>close instead."	socket ifNotNil:[socket destroy]! !!SocketStream methodsFor: 'stream out' stamp: 'nice 3/17/2010 20:27'!next: n putAll: aCollection startingAt: startIndex	"Put a String or a ByteArray onto the stream.	Currently a large collection will allocate a large buffer.	Warning: this does not work with WideString: they have to be converted first."	self adjustOutBuffer: n.	outBuffer replaceFrom: outNextToWrite to: outNextToWrite + n - 1 with: aCollection startingAt: startIndex.	outNextToWrite := outNextToWrite + n.	self checkFlush.	^aCollection! !!SocketStream methodsFor: 'stream out' stamp: 'nice 3/19/2010 19:14'!nextPutAll: aCollection	"Put a String or a ByteArray onto the stream.	Currently a large collection will allocate a large buffer."	| toPut |	toPut := binary ifTrue: [aCollection asByteArray] ifFalse: [aCollection asString].	self adjustOutBuffer: toPut size.	outBuffer replaceFrom: outNextToWrite to: outNextToWrite + toPut size - 1 with: toPut startingAt: 1.	outNextToWrite := outNextToWrite + toPut size.	self checkFlush.	^aCollection! !!SocketStream methodsFor: 'stream out' stamp: 'ar 7/24/2010 14:48'!nextPutAllFlush: aCollection	"Put a String or a ByteArray onto the stream.	You can use this if you have very large data - it avoids	copying into the buffer (and avoids buffer growing)	and also flushes any other pending data first."	| toPut |	toPut := binary ifTrue: [aCollection asByteArray] ifFalse: [aCollection asString].	self flush. "first flush pending stuff, then directly send"	socket isOtherEndClosed ifFalse: [		[self sendData: toPut count: toPut size]			on: ConnectionTimedOut			do: [:ex | shouldSignal ifFalse: ["swallow"]]]! !!SocketStream methodsFor: 'configuration' stamp: 'nice 3/16/2010 22:34'!ascii	"Tell the SocketStream to send data	as Strings instead of ByteArrays.	This is default."	binary := false.	inBuffer		ifNil: [self resetBuffers]		ifNotNil:			[inBuffer := inBuffer asString.			outBuffer := outBuffer asString]! !!SocketStream methodsFor: 'configuration' stamp: 'gk 2/9/2005 22:26'!autoFlush	"If autoFlush is enabled data will be sent through	the socket (flushed) when the bufferSize is reached	or the SocketStream is closed. Otherwise the user	will have to send #flush manually.	Close will always flush. Default is false."	^autoFlush! !!SocketStream methodsFor: 'configuration' stamp: 'gk 2/9/2005 22:27'!autoFlush: aBoolean	"If autoFlush is enabled data will be sent through	the socket (flushed) when the bufferSize is reached	or the SocketStream is closed. Otherwise the user	will have to send #flush manually.	Close will always flush. Default is false."	autoFlush := aBoolean! !!SocketStream methodsFor: 'configuration' stamp: 'nice 3/16/2010 22:35'!binary	"Tell the SocketStream to send data	as ByteArrays instead of Strings.	Default is ascii."	binary := true.	inBuffer		ifNil: [self resetBuffers]		ifNotNil:			[inBuffer := inBuffer asByteArray.			outBuffer := outBuffer asByteArray]! !!SocketStream methodsFor: 'configuration' stamp: 'gk 2/9/2005 22:28'!bufferSize: anInt	"Default buffer size is 4kb.	increased from earlier 2000 bytes."	bufferSize := anInt! !!SocketStream methodsFor: 'configuration' stamp: 'gk 2/10/2005 17:59'!noTimeout	"Do not use timeout."	timeout := 0! !!SocketStream methodsFor: 'configuration' stamp: 'gk 2/3/2005 20:35'!socket: aSocket	socket := aSocket! !!SocketStream methodsFor: 'configuration' stamp: 'gk 2/7/2005 08:41'!timeout	"Lazily initialized unless it has been set explicitly."	timeout ifNil: [timeout := Socket standardTimeout].	^timeout! !!SocketStream methodsFor: 'control' stamp: 'ar 7/24/2010 14:48'!flush	"If the other end is connected and we have something	to send, then we send it and reset the outBuffer."	((outNextToWrite > 1) and: [socket isOtherEndClosed not])		ifTrue: [			[self sendData: outBuffer count: outNextToWrite - 1]				on: ConnectionTimedOut				do: [:ex | shouldSignal ifFalse: ["swallow"]].			outNextToWrite := 1]! !!SocketStream methodsFor: 'control' stamp: 'ar 8/5/2010 12:23'!receiveData: nBytes	"Keep reading the socket until we have nBytes	in the inBuffer or we reach the end. This method	does not return data, but can be used to make sure	data has been read into the buffer from the Socket	before actually reading it from the FastSocketStream.	Mainly used internally. We could also adjust the buffer	to the expected amount of data and avoiding several	incremental grow operations.	NOTE: This method doesn't honor timeouts if shouldSignal	is false!! And frankly, I am not sure how to handle that	case or if I care - I think we should always signal."	[self isConnected and: [nBytes > self inBufferSize]]		whileTrue: [self receiveData]! !!SocketStream methodsFor: 'private-socket' stamp: 'ar 7/24/2010 15:07'!receiveAvailableData	"Receive available data (as much as fits in the inBuffer) but not waiting for more to arrive. Return the position in the buffer where the new data starts, regardless if anything was read, see #adjustInBuffer."		recentlyRead := self receiveDataInto: inBuffer startingAt: inNextToWrite.	^self adjustInBuffer: recentlyRead! !!SocketStream methodsFor: 'private-socket' stamp: 'ar 7/24/2010 15:03'!receiveData	"Receive data. Signal exceptions and timeouts depending on #shouldSignal and #shouldTimeout. Return the position in the buffer where the new data starts, regardless if anything was read."	socket		waitForDataFor: self timeout		ifClosed: [self shouldSignal 			ifTrue:[ConnectionClosed signal: 'Connection closed while waiting for data.']]		ifTimedOut: [self shouldTimeout			ifTrue:[ConnectionTimedOut signal: 'Data receive timed out.']].	^self receiveAvailableData! !!SocketStream methodsFor: 'private-socket' stamp: 'ar 7/24/2010 14:50'!receiveDataIfAvailable	"Deprecated. Use #receiveAvailableData instead"	^self receiveAvailableData! !!SocketStream methodsFor: 'private-socket' stamp: 'ar 7/24/2010 15:07'!receiveDataInto: buffer startingAt: index.	"Read data from the underlying socket. This method may be overridden by subclasses wanting to control incoming traffic for other purposes like encryption or statistics."	^socket  receiveAvailableDataInto: buffer startingAt: index.! !!SocketStream methodsFor: 'private-socket' stamp: 'ar 7/24/2010 14:50'!sendData: buffer count: n	"Sends outgoing data directly on the underlying socket."	^socket sendData: buffer count: n! !!SocketStream class methodsFor: 'instance creation' stamp: 'gk 2/3/2005 22:19'!on: socket	"Create a socket stream on a connected server socket."	^self basicNew initialize socket: socket! !!SocketStream class methodsFor: 'instance creation' stamp: 'gk 2/3/2005 20:35'!openConnectionToHost: hostIP port: portNumber	| socket |	socket := Socket new.	socket connectTo: hostIP port: portNumber.	^self on: socket! !!SocketStream class methodsFor: 'instance creation' stamp: 'gk 2/3/2005 20:35'!openConnectionToHostNamed: hostName port: portNumber	| hostIP |	hostIP := NetNameResolver addressForName: hostName timeout: 20.	^self openConnectionToHost: hostIP port: portNumber! !!SocketStream class methodsFor: 'example' stamp: 'md 8/14/2005 18:25'!finger: userName	"SocketStream finger: 'stp'"	| addr s |	addr := NetNameResolver promptUserForHostAddress.	s := SocketStream openConnectionToHost: addr port: 79.  "finger port number"	Transcript show: '---------- Connecting ----------'; cr.	s sendCommand: userName.	Transcript show: s getLine.	s close.	Transcript show: '---------- Connection Closed ----------'; cr; endEntry.! !