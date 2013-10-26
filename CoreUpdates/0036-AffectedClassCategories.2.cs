'From Squeak3.7 of ''4 September 2004'' [latest update: #5989] on 9 July 2008 at 2:25:54 pm'!!ChangeSet methodsFor: 'class changes' stamp: 'jmv 7/9/2008 14:25'!changedClassCategories	| answer |	answer _ Dictionary new.	self changedClasses do: [ :cls | 		(answer at: cls theNonMetaClass category ifAbsentPut: Set new)		add: cls ].	^ answer		! !!ChangeSorter methodsFor: 'changeSet menu' stamp: 'jmv 7/9/2008 14:06'!shiftedChangeSetMenu: aMenu	"Set up aMenu to hold items relating to the change-set-list pane when the shift key is down"	aMenu title: 'Change set (shifted)'.		aMenu addStayUpItemSpecial.	"CONFLICTS SECTION"	aMenu add: 'conflicts with other change sets' action: #browseMethodConflicts.	aMenu balloonTextForLastItem: 'Browse all methods that occur both in this change set and in at least one other change set.'.	parent ifNotNil:		[aMenu add: 'conflicts with change set opposite' action: #methodConflictsWithOtherSide.			aMenu balloonTextForLastItem: 'Browse all methods that occur both in this change set and in the one on the opposite side of the change sorter.'.			aMenu add: 'conflicts with category opposite' action: #methodConflictsWithOppositeCategory.			aMenu balloonTextForLastItem: 'Browse all methods that occur both in this change set and in ANY change set in the category list on the opposite side of this change sorter, other of course than this change set itself.  (Caution -- this could be VERY slow)'].	aMenu addLine.	"CHECKS SECTION"	aMenu add: 'check for slips' action: #lookForSlips.	aMenu balloonTextForLastItem: 'Check this change set for halts and references to Transcript.'.	aMenu add: 'check for unsent messages' action: #checkForUnsentMessages.	aMenu balloonTextForLastItem:'Check this change set for messages that are not sent anywhere in the system'.	aMenu add: 'check for uncommented methods' action: #checkForUncommentedMethods.	aMenu balloonTextForLastItem:'Check this change set for methods that do not have comments'.	aMenu add: 'check for uncommented classes' action: #checkForUncommentedClasses.	aMenu balloonTextForLastItem:'Check for classes with code in this changeset which lack class comments'.	Utilities authorInitialsPerSe isEmptyOrNil ifFalse:		[aMenu add: 'check for other authors' action: #checkForAlienAuthorship.		aMenu balloonTextForLastItem:'Check this change set for methods whose current authoring stamp does not start with "', Utilities authorInitials, '"'.	aMenu add: 'check for any other authors' action: #checkForAnyAlienAuthorship.	aMenu balloonTextForLastItem:'Check this change set for methods any of whose authoring stamps do not start with "', Utilities authorInitials, '"'].	aMenu add: 'check for uncategorized methods' action: #checkForUnclassifiedMethods.	aMenu balloonTextForLastItem:'Check to see if any methods in the selected change set have not yet been assigned to a category.  If any are found, open a browser on them.'.	aMenu addLine.	aMenu add: 'inspect change set' action: #inspectChangeSet.	aMenu balloonTextForLastItem: 'Open an inspector on this change set. (There are some details in a change set which you don''t see in a change sorter.)'.	aMenu add: 'update' action: #update.	aMenu balloonTextForLastItem: 'Update the display for this change set.  (This is done automatically when you activate this window, so is seldom needed.)'.	aMenu add: 'promote to top of list' action: #promoteToTopChangeSet.	aMenu balloonTextForLastItem:'Make this change set appear first in change-set lists in all change sorters.'.	aMenu add: 'trim history' action: #trimHistory.	aMenu balloonTextForLastItem: ' Drops any methods added and then removed, as well as renaming and reorganization of newly-added classes.  NOTE: can cause confusion if later filed in over an earlier version of these changes'.	aMenu add: 'view affected class categories' action: #viewAffectedClassCategories.	aMenu balloonTextForLastItem: ' Show class categories affected by any contained change'.		aMenu add: 'remove contained in class categories...' action: #removeContainedInClassCategories.	aMenu balloonTextForLastItem: ' Drops any changes in given class categories'.	aMenu add: 'clear this change set' action: #clearChangeSet.	aMenu balloonTextForLastItem: 'Reset this change set to a pristine state where it holds no information. CAUTION: this is destructive and irreversible!!'.	aMenu add: 'expunge uniclasses' action: #expungeUniclasses.	aMenu balloonTextForLastItem:'Remove from the change set all memory of uniclasses, e.g. classes added on behalf of etoys, fabrik, etc., whose classnames end with a digit.'.	aMenu add: 'uninstall this change set' action: #uninstallChangeSet.	aMenu balloonTextForLastItem: 'Attempt to uninstall this change set. CAUTION: this may not work completely and is irreversible!!'.	aMenu addLine.	aMenu add: 'file into new...' action: #fileIntoNewChangeSet.	aMenu balloonTextForLastItem: 'Load a fileout from disk and place its changes into a new change set (seldom needed -- much better to do this from a file-list browser these days.)'.	aMenu add: 'reorder all change sets' action: #reorderChangeSets.	aMenu balloonTextForLastItem:'Applies a standard reordering of all change-sets in the system -- at the bottom will come the sets that come with the release; next will come all the numbered updates; finally, at the top, will come all other change sets'.	aMenu addLine.	aMenu add: 'more...' action: #offerUnshiftedChangeSetMenu.	aMenu balloonTextForLastItem: 'Takes you back to the primary change-set menu.'.	^ aMenu! !!ChangeSorter methodsFor: 'changeSet menu' stamp: 'jmv 7/9/2008 14:06'!viewAffectedClassCategories	myChangeSet changedClassCategories explore! !