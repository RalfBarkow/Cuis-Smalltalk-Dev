'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 13 April 2011 at 8:34:49 am'!!classDefinition: #FillInTheBlankMorph category: #'Morphic-Widgets'!Morph subclass: #FillInTheBlankMorph	instanceVariableNames: 'response done textPane responseUponCancel'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Widgets'!!classDefinition: #HaloHandleMorph category: #'Morphic-Halos'!Morph subclass: #HaloHandleMorph	instanceVariableNames: ''	classVariableNames: 'CircleForm'	poolDictionaries: ''	category: 'Morphic-Halos'!!classDefinition: #HaloMorph category: #'Morphic-Halos'!Morph subclass: #HaloMorph	instanceVariableNames: 'target innerTarget positionOffset angleOffset growingOrRotating haloBox'	classVariableNames: 'HandleSize Icons'	poolDictionaries: ''	category: 'Morphic-Halos'!!classDefinition: #HaloSpec category: #'Morphic-Halos'!Object subclass: #HaloSpec	instanceVariableNames: 'addHandleSelector horizontalPlacement verticalPlacement color iconSymbol'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Halos'!!classDefinition: #LazyListMorph category: #'Morphic-Support'!Morph subclass: #LazyListMorph	instanceVariableNames: 'listItems font selectedRow selectedRows listSource'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Support'!!classDefinition: #MagnifierMorph category: #'Morphic-Widgets'!BorderedMorph subclass: #MagnifierMorph	instanceVariableNames: 'magnification trackPointer lastPos srcExtent auxCanvas magnifiedForm'	classVariableNames: 'RecursionLock'	poolDictionaries: ''	category: 'Morphic-Widgets'!!classDefinition: #OneLineEditorMorph category: #'Morphic-Widgets'!Morph subclass: #OneLineEditorMorph	instanceVariableNames: 'font emphasis contents editor showCaret pauseBlinking caretRect keyboardFocusWatcher'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Widgets'!!classDefinition: #PluggableMorph category: #'Morphic-Views for Models'!BorderedMorph subclass: #PluggableMorph	instanceVariableNames: 'model'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!PluggableMorph commentStamp: '<historical>' prior: 0!PluggableMorph are used to represent structures with state and behavior as well as graphical structure.  A PluggableMorph is usually the root of a morphic tree depicting its appearance.  The tree is constructed concretely by adding its consituent morphs to a world.When a part is named in a world, it is given a new slot in the model.  When a part is sensitized, it is named, and a set of mouse-driven methods is also generated in the model.  These may be edited to induce particular behavior.  When a variable is added through the morphic world, it is given a slot in the model, along with a set of access methods.In addition for public variables (and this is the default for now), methods are generated and called in any outer model in which this model gets embedded, thus propagating variable changes outward.!!classDefinition: #PluggableButtonMorph category: #'Morphic-Views for Models'!PluggableMorph subclass: #PluggableButtonMorph	instanceVariableNames: 'label font icon getStateSelector actionSelector isPressed mouseIsOver magnifiedIcon actWhen'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #ScrollBar category: #'Morphic-Views for Models'!PluggableMorph subclass: #ScrollBar	instanceVariableNames: 'slider value setValueSelector sliderShadow sliderColor menuButton upButton downButton pagingArea scrollDelta pageDelta interval menuSelector timeOfMouseDown timeOfLastScroll nextPageDirection currentScrollDelay scrollBarAction'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #ScrollPane category: #'Morphic-Views for Models'!PluggableMorph subclass: #ScrollPane	instanceVariableNames: 'scrollBar scroller hScrollBar hideScrollBars currentScrollRange drawKeyboardFocusIndicator'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #PluggableListMorph category: #'Morphic-Views for Models'!ScrollPane subclass: #PluggableListMorph	instanceVariableNames: 'list getListSelector getListSizeSelector getIndexSelector setIndexSelector keystrokeActionSelector autoDeselect lastKeystrokeTime lastKeystrokes doubleClickSelector handlesBasicKeys potentialDropRow listMorph menuGetter'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #PluggableListMorphByItem category: #'Morphic-Views for Models'!PluggableListMorph subclass: #PluggableListMorphByItem	instanceVariableNames: 'itemList'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #PluggableListMorphOfMany category: #'Morphic-Views for Models'!PluggableListMorph subclass: #PluggableListMorphOfMany	instanceVariableNames: 'dragOnOrOff getSelectionListSelector setSelectionListSelector dragStartRow'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #PluggableMessageCategoryListMorph category: #'Morphic-Views for Models'!PluggableListMorph subclass: #PluggableMessageCategoryListMorph	instanceVariableNames: 'getRawListSelector priorRawList'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #ScrollbarButton category: #'Morphic-Support'!BorderedMorph subclass: #ScrollbarButton	instanceVariableNames: 'image'	classVariableNames: 'Arrows Box CurrentSize'	poolDictionaries: ''	category: 'Morphic-Support'!!classDefinition: #SimpleHierarchicalListMorph category: #'Morphic-Views for Models'!ScrollPane subclass: #SimpleHierarchicalListMorph	instanceVariableNames: 'selectedMorph getListSelector keystrokeActionSelector autoDeselect columns sortingSelector getSelectionSelector setSelectionSelector potentialDropMorph lineColor menuGetter'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #SketchMorph category: #'Morphic-Widgets'!Morph subclass: #SketchMorph	instanceVariableNames: 'originalForm'	classVariableNames: 'PaintingIcon'	poolDictionaries: ''	category: 'Morphic-Widgets'!!classDefinition: #StarMorph category: #'Morphic-Basic'!PolygonMorph subclass: #StarMorph	instanceVariableNames: ''	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Basic'!!classDefinition: #SystemWindow category: #'Morphic-Views for Models'!PluggableMorph subclass: #SystemWindow	instanceVariableNames: 'labelString collapsedFrame fullFrame isCollapsed updatablePanes widgetsColor layoutMorph topAdjuster bottomAdjuster leftAdjuster rightAdjuster topLeftAdjuster bottomLeftAdjuster topRightAdjuster bottomRightAdjuster'	classVariableNames: 'CloseIcon CollapseIcon ExpandIcon MenuIcon TopWindow'	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #TextModelMorph category: #'Morphic-Views for Models'!ScrollPane subclass: #TextModelMorph	instanceVariableNames: 'textMorph hasUnacceptedEdits askBeforeDiscardingEdits selectionInterval hasEditingConflicts editorClass styler'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #BrowserCommentTextMorph category: #'Morphic-Views for Models'!TextModelMorph subclass: #BrowserCommentTextMorph	instanceVariableNames: 'separator separatorHeight proportionalHeight'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #LimitedHeightTextMorph category: #'Morphic-Views for Models'!TextModelMorph subclass: #LimitedHeightTextMorph	instanceVariableNames: 'maxHeight'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!classDefinition: #TheWorldMenu category: #'Morphic-Menus'!Object subclass: #TheWorldMenu	instanceVariableNames: 'myWorld myHand'	classVariableNames: 'OpenMenuRegistry'	poolDictionaries: ''	category: 'Morphic-Menus'!!RectangleIndicatorMorph methodsFor: 'drawing' stamp: 'FernanodOlivero 4/13/2011 10:54'!defaultBorderWidth	^ 2 ! !!RectangleIndicatorMorph methodsFor: 'drawing' stamp: 'FernanodOlivero 4/13/2011 10:54'!drawOn: aCanvas	| bw |	bw _ self defaultBorderWidth.	aCanvas frameRectangle: bounds borderWidth: bw color: Color black.	aCanvas frameRectangle: (bounds insetBy: bw) borderWidth: bw color: Color white! !