'From Cuis 2.3 of 22 March 2010 [latest update: #472] on 26 April 2010 at 9:33:15 pm'!!SmalltalkEditor class methodsFor: 'keyboard shortcut tables' stamp: 'jmv 4/26/2010 21:26'!initializeCmdKeyShortcuts	"Initialize the (unshifted) command-key (or alt-key) shortcut table."	"NOTE: if you don't know what your keyboard generates, use Sensor kbdTest"	"SmalltalkEditor initialize"	| cmds |	self initializeBasicCmdKeyShortcuts.		cmds := #($b #browseIt: $d #doIt: $i #inspectIt: $j #doAgainOnce: $l #cancel: $m #implementorsOfIt: $n #sendersOfIt: $p #printIt: $q #querySymbol: $s #save: ).	1 to: cmds size		by: 2		do: [:i | cmdActions at: (cmds at: i) asciiValue + 1 put: (cmds at: i + 1)]! !!SmalltalkEditor class methodsFor: 'keyboard shortcut tables' stamp: 'jmv 4/26/2010 21:27'!initializeShiftedYellowButtonMenu	"Initialize the yellow button pop-up menu and corresponding messages."	"SmalltalkEditor initialize"	shiftedYellowButtonMenu _ SelectionMenu fromArray: {		{'explain' translated.						#explain}.		{'pretty print' translated.					#prettyPrint}.		{'file it in (G)' translated.					#fileItIn}.		#-.		{'browse it (b)' translated.					#browseIt}.		{'senders of it (n)' translated.				#sendersOfIt}.		{'implementors of it (m)' translated.		#implementorsOfIt}.		{'references to it (N)' translated.			#referencesToIt}.		#-.		{'selectors containing it (W)' translated.	#methodNamesContainingIt}.		{'method strings with it (E)' translated.		#methodStringsContainingit}.		{'method source with it' translated.		#methodSourceContainingIt}.		{'class names containing it' translated.		#classNamesContainingIt}.		{'class comments with it' translated.		#classCommentsContainingIt}.		{'change sets with it' translated.			#browseChangeSetsWithSelector}.		#-.		{'save contents to file...' translated.		#saveContentsInFile}.		#-.		{'more...' translated.						#yellowButtonActivity}.	}! !FileList removeSelector: #spawn:!Browser removeSelector: #spawn:!Browser removeSelector: #suggestCategoryToSpawnedBrowser:!CodeHolder removeSelector: #spawn:!CodeHolder removeSelector: #suggestCategoryToSpawnedBrowser:!StringHolder removeSelector: #spawn:!SmalltalkEditor removeSelector: #spawn!SmalltalkEditor removeSelector: #spawnIt:!PluggableTextMorph removeSelector: #spawn!"Postscript:Leave the line above, and replace the rest of this comment by a useful one.Executable statements should follow this comment, and shouldbe separated by periods, with no exclamation points (!!).Be sure to put any further comments in double-quotes, like this one."SmalltalkEditor initialize!