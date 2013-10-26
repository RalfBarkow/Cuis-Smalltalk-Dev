'From Cuis 1.0 of 6 August 2009 [latest update: #258] on 21 August 2009 at 10:27:54 am'!!CodeHolder methodsFor: 'annotation' stamp: 'jmv 8/21/2009 09:27'!defaultButtonPaneHeight	"Answer the user's preferred default height for new button panes."	^Preferences standardButtonFont height * 8 // 5	! !!FileList class methodsFor: 'instance creation' stamp: 'jmv 8/21/2009 09:27'!defaultButtonPaneHeight	"Answer the user's preferred default height for new button panes."	^Preferences standardButtonFont height * 8 // 5! !!Preferences class methodsFor: 'fonts' stamp: 'jmv 8/21/2009 08:54'!subPixelRenderColorFonts	^ self		valueOfFlag: #subPixelRenderColorFonts		ifAbsent: [true]! !!Preferences class methodsFor: 'fonts' stamp: 'jmv 8/21/2009 08:55'!subPixelRenderFonts	^ self		valueOfFlag: #subPixelRenderFonts		ifAbsent: [true]! !!Preferences class methodsFor: 'misc' stamp: 'jmv 8/21/2009 09:34'!defaultValueTableForCurrentRelease	"Answer a table defining default values for all the preferences in the release.  Returns a list of (pref-symbol, boolean-symbol) pairs"	^  #(		(abbreviatedBrowserButtons false)		(alternativeBrowseIt false)		(alternativeWindowLook true)		(annotationPanes false)		(automaticFlapLayout true)		(automaticPlatformSettings true)		(balloonHelpEnabled true)		(browseWithPrettyPrint false)		(browserShowsPackagePane false)		(canRecordWhilePlaying false)		(caseSensitiveFinds false)		(changeSetVersionNumbers true)		(checkForSlips true)		(classicNewMorphMenu false)		(cmdDotEnabled true)		(collapseWindowsInPlace false)		(conversionMethodsAtFileOut false)		(cpuWatcherEnabled false)		(debugHaloHandle true)		(debugPrintSpaceLog false)		(debugShowDamage false)		(decorateBrowserButtons true)		(diffsInChangeList true)		(diffsWithPrettyPrint false)		(dismissAllOnOptionClose false)		(fastDragWindowForMorphic true)		(fullScreenLeavesDeskMargins true)		(hiddenScrollBars false)		(higherPerformance false)		(honorDesktopCmdKeys true)		(inboardScrollbars true)		(logDebuggerStackToFile true)		(menuButtonInToolPane false)		(menuColorFromWorld false)		(menuKeyboardControl false)  		(modalColorPickers true)		(optionalButtons true)		(personalizedWorldMenu true)		(projectsSentToDisk false)		(restartAlsoProceeds false)		(reverseWindowStagger true)		(scrollBarsWithoutMenuButton false)		(selectiveHalos false)		(showBoundsInHalo false)		(simpleMenus false)		(smartUpdating true)		(soundQuickStart false)		(soundStopWhenDone false)		(soundsEnabled true)		(systemWindowEmbedOK false)		(thoroughSenders true)		(twentyFourHourFileStamps true)		(warnIfNoChangesFile true)		(warnIfNoSourcesFile true))"Preferences defaultValueTableForCurrentRelease do:	[:pair | (Preferences preferenceAt: pair first ifAbsent: [nil]) ifNotNilDo:			[:pref | pref defaultValue: (pair last == #true)]].Preferences chooseInitialSettings."! !!Preferences class methodsFor: 'scrollbar parameters' stamp: 'jmv 8/21/2009 09:35'!scrollbarThickness	"Includes border"	| result |	result _ Preferences standardListFont pointSize + 3.	self inboardScrollbars		ifFalse: [ result _ result + 2 ].		^ result! !!Preferences class methodsFor: 'themes' stamp: 'jmv 8/21/2009 09:34'!brightSqueak	"The classic bright Squeak look.  Windows have saturated colors and relatively low contrast; scroll-bars are of the flop-out variety and are on the left.  Many power-user features are enabled."	self setPreferencesFrom:	#(		(alternativeWindowLook false)		(annotationPanes true)		(automaticFlapLayout true)		(balloonHelpEnabled true)		(browseWithPrettyPrint false)		(browserShowsPackagePane false)		(classicNewMorphMenu false)		(cmdDotEnabled true)		(collapseWindowsInPlace false)		(debugHaloHandle true)		(debugPrintSpaceLog false)		(debugShowDamage false)		(decorateBrowserButtons true)		(diffsInChangeList true)		(diffsWithPrettyPrint false)		(fastDragWindowForMorphic true)		(fullScreenLeavesDeskMargins true)		(hiddenScrollBars false)		(inboardScrollbars false)		(logDebuggerStackToFile true)		(menuButtonInToolPane false)		(menuColorFromWorld false)		(menuKeyboardControl true)		(optionalButtons true)		(personalizedWorldMenu true)		(restartAlsoProceeds false)		(reverseWindowStagger true)		(scrollBarsWithoutMenuButton false)		(selectiveHalos false)		(simpleMenus false)		(smartUpdating true)		(systemWindowEmbedOK false)		(thoroughSenders true)		(warnIfNoChangesFile true)		(warnIfNoSourcesFile true))! !!Preferences class methodsFor: 'themes' stamp: 'jmv 8/21/2009 09:35'!juans	self setPreferencesFrom:	#(		(alternativeWindowLook true)		(annotationPanes true)		(balloonHelpEnabled false)		(browseWithPrettyPrint false)		(browserShowsPackagePane false)		(caseSensitiveFinds true)		(checkForSlips true)		(cmdDotEnabled true)		(collapseWindowsInPlace false)		(diffsInChangeList true)		(diffsWithPrettyPrint false)		(fastDragWindowForMorphic true)		(honorDesktopCmdKeys false)		(inboardScrollbars true)		(menuColorFromWorld false)		(menuKeyboardControl true)		(optionalButtons true)		(personalizedWorldMenu false)		(restartAlsoProceeds false)		(scrollBarsWithoutMenuButton false)		(simpleMenus false)		(smartUpdating true)		(subPixelRenderFonts true)		(thoroughSenders true)	)! !!Preferences class methodsFor: 'themes' stamp: 'jmv 8/21/2009 09:35'!paloAlto	"Similar to the brightSqueak theme, but with a number of idiosyncratic personal settings.   Note that caseSensitiveFinds is true"	self setPreferencesFrom:	#(		(abbreviatedBrowserButtons false)		(accessOnlineModuleRepositories noOpinion)		(alternativeBrowseIt noOpinion)		(alternativeWindowLook false)		(annotationPanes true)		(automaticFlapLayout true)		(automaticPlatformSettings noOpinion)		(balloonHelpEnabled true)		(browseWithPrettyPrint false)		(browserShowsPackagePane false)		(canRecordWhilePlaying noOpinion)		(caseSensitiveFinds true)		(changeSetVersionNumbers true)		(checkForSlips true)		(classicNewMorphMenu false)		(cmdDotEnabled true)		(collapseWindowsInPlace false)		(conservativeModuleDeActivation noOpinion)		(conversionMethodsAtFileOut true)		(cpuWatcherEnabled noOpinion)		(debugHaloHandle true)		(debugPrintSpaceLog true)		(debugShowDamage false)		(decorateBrowserButtons true)		(diffsInChangeList true)		(diffsWithPrettyPrint false)		(dismissAllOnOptionClose true)		(duplicateControlAndAltKeys false)		(extraDebuggerButtons true)		(fastDragWindowForMorphic true)		(fullScreenLeavesDeskMargins true)		(hiddenScrollBars false)		(higherPerformance noOpinion)		(honorDesktopCmdKeys true)		(inboardScrollbars false)		(logDebuggerStackToFile true)		(menuButtonInToolPane false)		(menuColorFromWorld false)		(menuKeyboardControl true)  		(modalColorPickers true)		(modularClassDefinitions noOpinion)		(optionalButtons true)		(personalizedWorldMenu true)		(projectsSentToDisk noOpinion)		(restartAlsoProceeds false)		(reverseWindowStagger true)		(scrollBarsWithoutMenuButton false)		(selectiveHalos false)		(showBoundsInHalo false)		(simpleMenus false)		(smartUpdating true)		(soundQuickStart noOpinion)		(soundsEnabled true)		(soundStopWhenDone noOpinion)		(strongModules noOpinion)		(swapControlAndAltKeys noOpinion)		(swapMouseButtons  noOpinion)		(systemWindowEmbedOK false)		(thoroughSenders true)		(twentyFourHourFileStamps false)		(warnIfNoChangesFile true)		(warnIfNoSourcesFile true))! !!Preferences class methodsFor: 'themes' stamp: 'jmv 8/21/2009 09:35'!slowMachine	self setPreferencesFrom:	#(		(alternativeWindowLook false)		(annotationPanes false)		(balloonHelpEnabled false)		(browseWithPrettyPrint false)		(browserShowsPackagePane false)		(caseSensitiveFinds true)		(checkForSlips false)		(cmdDotEnabled true)		(collapseWindowsInPlace false)		(diffsInChangeList false)		(diffsWithPrettyPrint false)		(fastDragWindowForMorphic true)		(honorDesktopCmdKeys false)		(inboardScrollbars true)		(menuColorFromWorld false)		(menuKeyboardControl false)		(optionalButtons false)		(personalizedWorldMenu false)		(restartAlsoProceeds false)		(scrollBarsWithoutMenuButton false)		(simpleMenus false)		(smartUpdating false)		(subPixelRenderFonts false)		(thoroughSenders false)	)! !!Preferences class methodsFor: 'themes' stamp: 'jmv 8/21/2009 09:35'!smalltalk80	"A traditional monochrome Smalltalk-80 look and feel, clean and austere, and lacking many features added to Squeak in recent years. Caution: this theme removes the standard Squeak flaps, turns off the 'smartUpdating' feature that keeps multiple browsers in synch, and much more."	self setPreferencesFrom:	#(		(alternativeWindowLook false)		(annotationPanes false)		(balloonHelpEnabled false)		(browseWithPrettyPrint false)		(browserShowsPackagePane false)		(caseSensitiveFinds true)		(checkForSlips false)		(cmdDotEnabled true)		(collapseWindowsInPlace false)		(diffsInChangeList false)		(diffsWithPrettyPrint false)		(fastDragWindowForMorphic true)		(honorDesktopCmdKeys false)		(inboardScrollbars false)		(menuColorFromWorld false)		(menuKeyboardControl false)		(optionalButtons false)		(personalizedWorldMenu false)		(restartAlsoProceeds false)		(scrollBarsWithoutMenuButton false)		(simpleMenus false)		(smartUpdating false)		(thoroughSenders false)	)! !!Preferences class methodsFor: 'shout' stamp: 'jmv 8/21/2009 08:54'!syntaxHighlightingAsYouType	^ self		valueOfFlag: #syntaxHighlightingAsYouType		ifAbsent: [true]! !!Preferences class methodsFor: 'shout' stamp: 'jmv 8/21/2009 08:55'!syntaxHighlightingAsYouTypeAnsiAssignment	^ self		valueOfFlag: #syntaxHighlightingAsYouTypeAnsiAssignment		ifAbsent: [false]! !!Preferences class methodsFor: 'shout' stamp: 'jmv 8/21/2009 08:56'!syntaxHighlightingAsYouTypeLeftArrowAssignment	^ self		valueOfFlag: #syntaxHighlightingAsYouTypeLeftArrowAssignment		ifAbsent: [false]! !!Preferences class methodsFor: 'bigger and smaller GUI' stamp: 'jmv 8/21/2009 09:34'!bigFonts	"Sets not only fonts but other GUI elements	to fit high resolution or large screens	Preferences bigFonts	"		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 11)		(setListFontTo: 'DejaVu' 11)		(setMenuFontTo: 'DejaVu' 12)		(setWindowTitleFontTo: 'DejaVu' 14)		(setBalloonHelpFontTo: 'DejaVu' 9)		(setCodeFontTo: 'DejaVu' 11)		(setButtonFontTo: 'DejaVu' 11))! !!Preferences class methodsFor: 'bigger and smaller GUI' stamp: 'jmv 8/21/2009 10:00'!hugeFonts	"Sets not only fonts but other GUI elements	to fit very high resolution or very large screens	Preferences hugeFonts	"		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 17)		(setListFontTo: 'DejaVu' 17)		(setMenuFontTo: 'DejaVu' 17)		(setWindowTitleFontTo: 'DejaVu' 22)		(setBalloonHelpFontTo: 'DejaVu' 14)		(setCodeFontTo: 'DejaVu' 17)		(setButtonFontTo: 'DejaVu' 17))! !!Preferences class methodsFor: 'bigger and smaller GUI' stamp: 'jmv 8/21/2009 09:35'!smallFonts	"Sets not only fonts but other GUI elements	to fit low resolution or small screens	Preferences smallFonts	"		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 8)		(setListFontTo: 'DejaVu' 7)		(setMenuFontTo: 'DejaVu' 7)		(setWindowTitleFontTo: 'DejaVu' 9)		(setBalloonHelpFontTo: 'DejaVu' 7)		(setCodeFontTo: 'DejaVu' 7)		(setButtonFontTo: 'DejaVu' 7))! !!Preferences class methodsFor: 'bigger and smaller GUI' stamp: 'jmv 8/21/2009 09:35'!standardFonts	"Sets not only fonts but other GUI elements	to fit regular resolution and size screens	Preferences standardFonts	"		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 9)		(setListFontTo: 'DejaVu' 9)		(setMenuFontTo: 'DejaVu' 10)		(setWindowTitleFontTo: 'DejaVu' 12)		(setBalloonHelpFontTo: 'DejaVu' 8)		(setCodeFontTo: 'DejaVu' 9)		(setButtonFontTo: 'DejaVu' 9))! !!Preferences class methodsFor: 'bigger and smaller GUI' stamp: 'jmv 8/21/2009 09:35'!tinyFonts	"Sets not only fonts but other GUI elements	to fit very low resolution or very small screens	Preferences tinyFonts	"		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 7)		(setListFontTo: 'DejaVu' 5)		(setMenuFontTo: 'DejaVu' 5)		(setWindowTitleFontTo: 'DejaVu' 7)		(setBalloonHelpFontTo: 'DejaVu' 5)		(setCodeFontTo: 'DejaVu' 5)		(setButtonFontTo: 'DejaVu' 5))! !!Preferences class methodsFor: 'bigger and smaller GUI' stamp: 'jmv 8/21/2009 09:53'!veryBigFonts	"Sets not only fonts but other GUI elements	to fit very high resolution or very large screens	Preferences veryBigFonts	"		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 14)		(setListFontTo: 'DejaVu' 14)		(setMenuFontTo: 'DejaVu' 14)		(setWindowTitleFontTo: 'DejaVu' 17)		(setBalloonHelpFontTo: 'DejaVu' 11)		(setCodeFontTo: 'DejaVu' 14)		(setButtonFontTo: 'DejaVu' 14))! !!StrikeFont class methodsFor: 'instance creation' stamp: 'jmv 8/21/2009 08:51'!installDejaVu"StrikeFont installDejaVu"	| dejaVuDict |	dejaVuDict _ Dictionary new.	5 to: 24 do: [ :s |		dejaVuDict at: s put: (self createDejaVu: s) ].	AvailableFonts at: 'DejaVu' put: dejaVuDict.	Preferences restoreDefaultFonts! !!StrikeFont class methodsFor: 'removing' stamp: 'jmv 8/21/2009 10:06'!removeSomeFonts"StrikeFont removeSomeFonts"	| familyDict |	familyDict _ AvailableFonts at: 'DejaVu'.	familyDict keys copy do: [ :k |		"No boldItalic for the followint"		(#(5 6 7 8 9 10 11 12 14 17 22) includes: k)			ifTrue: [ (familyDict at: k) derivativeFont: nil at: 3 ].		"No derivatives at all for the following"		(#() includes: k)			ifTrue: [ (familyDict at: k) derivativeFont: nil at: 0 ].		"Sizes to keep"		(#(5 6 7 8 9 10 11 12 14 17 22) includes: k) 			ifFalse: [ familyDict removeKey: k ]].		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 9)		(setListFontTo: 'DejaVu' 9)		(setMenuFontTo: 'DejaVu' 10)		(setWindowTitleFontTo: 'DejaVu' 12)		(setBalloonHelpFontTo: 'DejaVu' 8)		(setCodeFontTo: 'DejaVu' 9)		(setButtonFontTo: 'DejaVu' 9))! !!SystemWindow methodsFor: 'initialization' stamp: 'jmv 8/21/2009 09:17'!boxExtent	"answer the extent to use in all the buttons. 	 	the label height is used to be proportional to the fonts preferences"	| e |	e _ Preferences windowTitleFont height.	^e@e! !!TextStyle class methodsFor: 'examples' stamp: 'jmv 8/21/2009 10:27'!createExamples	"	TextStyle createExamples	"	| dejaVu17 dejaVu14 dejaVu11 dejaVu10 heading1 heading2 heading3 emphasized normal |	dejaVu17 _ AbstractFont familyName: 'DejaVu' pointSize: 17.	dejaVu14 _ AbstractFont familyName: 'DejaVu' pointSize: 14.	dejaVu11 _ AbstractFont familyName: 'DejaVu' pointSize: 11.	dejaVu10 _ AbstractFont familyName: 'DejaVu' pointSize: 10.		heading1 _ TextStyle withFont: dejaVu17 bold name: 'Heading 1' alignment: CharacterScanner centeredCode.	self makeAvailable: heading1.	heading2 _ TextStyle withFont: dejaVu17 italic name: 'Heading 2' alignment: CharacterScanner leftFlushCode.	self makeAvailable: heading2.	heading3 _ TextStyle withFont: dejaVu14 name: 'Heading 3' alignment: CharacterScanner leftFlushCode.	self makeAvailable: heading3.	emphasized _ TextStyle withFont: dejaVu10 bold name: 'Emphasized' alignment: CharacterScanner leftFlushCode.	self makeAvailable: emphasized.		normal _ TextStyle withFont: dejaVu11 name: 'Normal' alignment: CharacterScanner justifiedCode.	self makeAvailable: normal.! !!Preferences class reorganize!('add preferences' addPreference:categories:default:balloonHelp: addPreference:categories:default:balloonHelp:changeInformee:changeSelector: addPreference:category:default:balloonHelp:)('fonts' chooseBalloonHelpFont chooseCodeFont chooseFontWithPrompt:andSendTo:withSelector:highlight: chooseListFont chooseMenuFont chooseSystemFont chooseWindowTitleFont fontConfigurationMenu printStandardSystemFonts properAlphaForBlackText restoreDefaultFonts setBalloonHelpFontTo: setButtonFontTo: setCodeFontTo: setDefaultFonts: setListFontTo: setMenuFontTo: setSystemFontTo: setWindowTitleFontTo: standardBalloonHelpFont standardButtonFont standardCodeFont standardListFont standardMenuFont subPixelRenderColorFonts subPixelRenderFonts windowTitleFont)('get/set' disable: doesNotUnderstand: enable: setPreference:toValue: togglePreference: valueOfFlag: valueOfFlag:ifAbsent:)('halos' haloSpecifications haloSpecificationsForWorld iconicHaloSpecifications installHaloSpecsFromArray: installHaloTheme: resetHaloSpecifications)('hard-coded prefs' browseToolClass cmdGesturesEnabled cmdKeysInText desktopMenuTitle)('initialization' chooseInitialSettings compileAccessMethodForPreference: initializeDictionaryOfPreferences removePreference: setPreferencesFrom:)('menu parameters' menuBorderColor menuBorderWidth menuColor menuLineColor menuTitleBorderColor menuTitleBorderWidth menuTitleColor restoreDefaultMenuParameters)('misc' addModelItemsToWindowMenu: cleanUp defaultValueTableForCurrentRelease inspectUnused menuColorString setFlag:toValue:during: soundEnablingString staggerPolicyString toggleMenuColorPolicy toggleSoundEnabling toggleWindowPolicy wantsChangeSetLogging)('parameters' annotationInfo defaultAnnotationRequests defaultAuthorName initializeParameters inspectParameters maxBalloonHelpLineLength parameterAt:ifAbsent: parameterAt:ifAbsentPut: setDefaultAnnotationInfo setParameter:to:)('personalization' compileHardCodedPref:enable: disableProgrammerFacilities enableProgrammerFacilities letUserPersonalizeMenu personalizeUserMenu:)('preference-object access' allPreferenceObjects preferenceAt: preferenceAt:ifAbsent:)('preferences panel' initialExtent inspectPreferences openPreferencesInspector)('reacting to change' annotationPanesChanged optionalButtonsChanged setNotificationParametersForStandardPreferences smartUpdatingChanged)('scrollbar parameters' inboardScrollbars scrollBarsNarrow scrollBarsOnRight scrollbarThickness)('standard queries' aaFontsColormapDepth abbreviatedBrowserButtons alternativeBrowseIt alternativeWindowLook annotationPanes automaticFlapLayout automaticPlatformSettings balloonHelpEnabled browseWithPrettyPrint browserShowsPackagePane canRecordWhilePlaying caseSensitiveFinds changeSetVersionNumbers checkForSlips classicNewMorphMenu cmdDotEnabled collapseWindowsInPlace conversionMethodsAtFileOut cpuWatcherEnabled debugHaloHandle debugLogTimestamp debugPrintSpaceLog debugShowDamage decorateBrowserButtons diffsInChangeList diffsWithPrettyPrint dismissAllOnOptionClose duplicateControlAndAltKeys extraDebuggerButtons fastDragWindowForMorphic focusFollowsMouse focusIndicatorColor focusIndicatorWidth fullScreenLeavesDeskMargins haloEnclosesFullBounds higherPerformance honorDesktopCmdKeys logDebuggerStackToFile menuAppearance3d menuButtonInToolPane menuColorFromWorld menuKeyboardControl menuWithIcons modalColorPickers optionalButtons personalizedWorldMenu projectsSentToDisk restartAlsoProceeds reverseWindowStagger scrollBarsWithoutMenuButton selectionsMayShrink selectiveHalos serverMode showBoundsInHalo showDeprecationWarnings showLinesInHierarchyViews simpleMenus smartUpdating soundQuickStart soundStopWhenDone soundsEnabled swapControlAndAltKeys swapMouseButtons thoroughSenders twentyFourHourFileStamps useFileList2 warnIfNoChangesFile warnIfNoSourcesFile wordStyleCursorMovement)('text highlighting' initializeTextHighlightingParameters insertionPointColor insertionPointColor: textHighlightColor)('themes' brightSqueak juans magdeburg outOfTheBox paloAlto slowMachine slowMachine1 smalltalk80 westwood)('shout' syntaxHighlightingAsYouType syntaxHighlightingAsYouTypeAnsiAssignment syntaxHighlightingAsYouTypeLeftArrowAssignment systemWindowEmbedOK)('bigger and smaller GUI' bigFonts hugeFonts smallFonts standardFonts tinyFonts veryBigFonts)!"Postscript:Leave the line above, and replace the rest of this comment by a useful one.Executable statements should follow this comment, and shouldbe separated by periods, with no exclamation points (!!).Be sure to put any further comments in double-quotes, like this one."!