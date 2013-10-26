'From Cuis 1.0 of 28 July 2009 [latest update: #242] on 30 July 2009 at 10:11:34 pm'!!PluggableTextMorph methodsFor: 'event handling' stamp: 'jmv 7/30/2009 20:20'!mouseEnter: event	super mouseEnter: event.	"I really don't know what is this for..."	self flag: #jmv.	"	selectionInterval ifNotNil: [		textMorph editor selectInterval: selectionInterval].	textMorph selectionChanged.	"	Preferences focusFollowsMouse		ifTrue: [ event hand newKeyboardFocus: textMorph ]! !!TextEditor methodsFor: 'editing keys' stamp: 'jmv 7/30/2009 18:20'!changeEmphasis: characterStream 	"Change the emphasis of the current selection or prepare to accept characters with the change in emphasis. Emphasis change amounts to a font change.  Keeps typeahead."	"control 0..9 -> 0..9"	| keyCode attribute oldAttributes index thisSel colors |	keyCode := ('0123456789-=' indexOf: sensor keyboard ifAbsent: [1]) - 1.	oldAttributes := paragraph text attributesAt: self pointIndex.	thisSel := self selection.	"Decipher keyCodes for Command 0-9..."	"	(keyCode between: 1 and: 5) 		ifTrue: [attribute := TextFontChange fontNumber: keyCode].	"	keyCode = 6 		ifTrue: [			colors := #(#black #magenta #red #yellow #green #blue #cyan #white).			index := (PopUpMenu 						labelArray: colors , #('choose color...' )						lines: (Array with: colors size + 1)) startUp.			index = 0 ifTrue: [^true].			index <= colors size 				ifTrue: [attribute := TextColor color: (Color perform: (colors at: index))]				ifFalse: [					index := index - colors size - 1.	"Re-number!!!!!!"					index = 0 ifTrue: [attribute := self chooseColor].					thisSel ifNil: [^true]	"Could not figure out what to link to"]].	(keyCode between: 7 and: 11) 		ifTrue: [			sensor leftShiftDown 				ifTrue: [					keyCode = 10 ifTrue: [attribute := TextKern kern: -1].					keyCode = 11 ifTrue: [attribute := TextKern kern: 1]]				ifFalse: [					attribute := TextEmphasis 								perform: (#(#bold #italic #narrow #underlined #struckOut) at: keyCode - 6).					oldAttributes 						do: [:att | (att dominates: attribute) ifTrue: [attribute turnOff]]]].	keyCode = 0 ifTrue: [attribute := TextEmphasis normal].	attribute ifNotNil: [		thisSel size = 0			ifTrue: [				"only change emphasisHere while typing"				self insertTypeAhead: characterStream.				emphasisHere _ Text addAttribute: attribute toArray: oldAttributes ]			ifFalse: [				self replaceSelectionWith: (thisSel asText addAttribute: attribute) ]].	^true! !!TextEditor methodsFor: 'new selection' stamp: 'jmv 7/30/2009 20:17'!selectFrom: start to: stop	"Select the specified characters inclusive."	self selectInvisiblyFrom: start to: stop.	self closeTypeIn.	self storeSelectionInParagraph.	"Preserve current emphasis if selection is empty"	stop > start ifTrue: [		self setEmphasisHere ]! !!SmalltalkEditor methodsFor: 'editing keys' stamp: 'jmv 7/30/2009 18:47'!changeEmphasis: characterStream 	"Change the emphasis of the current selection or prepare to accept characters with the change in emphasis. Emphasis change amounts to a font change.  Keeps typeahead."	"control 0..9 -> 0..9"	| keyCode attribute oldAttributes index thisSel colors extras |	keyCode := ('0123456789-=' indexOf: sensor keyboard ifAbsent: [1]) - 1.	oldAttributes := paragraph text attributesAt: self pointIndex.	thisSel := self selection.	"Decipher keyCodes for Command 0-9..."	"	(keyCode between: 1 and: 5) 		ifTrue: [attribute := TextFontChange fontNumber: keyCode].	"	keyCode = 6 		ifTrue: [			colors := #(#black #magenta #red #yellow #green #blue #cyan #white).			extras := #('Link to comment of class' 'Link to definition of class' 'Link to hierarchy of class' 'Link to method').			index := (PopUpMenu 						labelArray: colors , #('choose color...' 'Do it' 'Print it') , extras 								, #('be a web URL link' 'Edit hidden info' 'Copy hidden info')						lines: (Array with: colors size + 1)) startUp.			index = 0 ifTrue: [^true].			index <= colors size 				ifTrue: [attribute := TextColor color: (Color perform: (colors at: index))]				ifFalse: 					[index := index - colors size - 1.	"Re-number!!!!!!"					index = 0 ifTrue: [attribute := self chooseColor].					index = 1 						ifTrue: 							[attribute := TextDoIt new.							thisSel := attribute analyze: self selection asString].					index = 2 						ifTrue: 							[attribute := TextPrintIt new.							thisSel := attribute analyze: self selection asString].					extras size = 0 & (index > 2) ifTrue: [index := index + 5].	"skip those"					index = 3 						ifTrue: 							[attribute := TextLink new.							thisSel := attribute analyze: self selection asString with: 'Comment'].					index = 4 						ifTrue: 							[attribute := TextLink new.							thisSel := attribute analyze: self selection asString with: 'Definition'].					index = 5 						ifTrue: 							[attribute := TextLink new.							thisSel := attribute analyze: self selection asString with: 'Hierarchy'].					index = 6 						ifTrue: 							[attribute := TextLink new.							thisSel := attribute analyze: self selection asString].					index = 7 						ifTrue: 							[attribute := TextURL new.							thisSel := attribute analyze: self selection asString].					index = 8 						ifTrue: 							["Edit hidden info"							thisSel := self hiddenInfo.	"includes selection"							attribute := TextEmphasis normal].					index = 9 						ifTrue: 							["Copy hidden info"							self copyHiddenInfo.							^true].	"no other action"					thisSel ifNil: [^true]	"Could not figure out what to link to"]].	(keyCode between: 7 and: 11) 		ifTrue: [			sensor leftShiftDown 				ifTrue: [					keyCode = 10 ifTrue: [attribute := TextKern kern: -1].					keyCode = 11 ifTrue: [attribute := TextKern kern: 1]]				ifFalse: [					attribute := TextEmphasis 								perform: (#(#bold #italic #narrow #underlined #struckOut) at: keyCode - 6).					oldAttributes 						do: [:att | (att dominates: attribute) ifTrue: [attribute turnOff]]]].	keyCode = 0 ifTrue: [attribute := TextEmphasis normal].	attribute ifNotNil: [		thisSel size = 0			ifTrue: [				"only change emphasisHere while typing"				self insertTypeAhead: characterStream.				emphasisHere _ Text addAttribute: attribute toArray: oldAttributes ]			ifFalse: [				self replaceSelectionWith: (thisSel asText addAttribute: attribute) ]].	^true! !