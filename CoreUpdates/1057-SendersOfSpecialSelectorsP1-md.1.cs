'From Cuis 3.3 of 2 June 2011 [latest update: #1024] on 5 August 2011 at 11:04:30 pm'!!classDefinition: #Encoder category: #'Compiler-Kernel'!ParseNode subclass: #Encoder	instanceVariableNames: 'scopeTable nTemps supered requestor class selector literalStream selectorSet litIndSet litSet sourceRanges globalSourceRanges addedSelectorAndMethodClassLiterals optimizedSelectors '	classVariableNames: ''	poolDictionaries: ''	category: 'Compiler-Kernel'!!Encoder methodsFor: 'encoding' stamp: 'nice 3/30/2011 23:26'!noteOptimizedSelector: aSymbol	"Register a selector as being optimized.	These optimized selectors will later be registered into the literals so that tools can easily browse senders."	optimizedSelectors add: aSymbol! !!Encoder methodsFor: 'initialize-release' stamp: 'nice 3/30/2011 23:04'!initScopeAndLiteralTables	scopeTable := StdVariables copy.	litSet := StdLiterals copy.	"comments can be left hanging on nodes from previous compilations.	 probably better than this hack fix is to create the nodes afresh on each compilation."	scopeTable do:		[:varNode| varNode comment: nil].	litSet do:		[:varNode| varNode comment: nil].	selectorSet := StdSelectors copy.	litIndSet := Dictionary new: 16.	literalStream := WriteStream on: (Array new: 32).	addedSelectorAndMethodClassLiterals := false.	optimizedSelectors := Set new! !!classDefinition: #Encoder category: #'Compiler-Kernel'!ParseNode subclass: #Encoder	instanceVariableNames: 'scopeTable nTemps supered requestor class selector literalStream selectorSet litIndSet litSet sourceRanges globalSourceRanges addedSelectorAndMethodClassLiterals optimizedSelectors'	classVariableNames: ''	poolDictionaries: ''	category: 'Compiler-Kernel'!