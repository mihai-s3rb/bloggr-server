import React, { useMemo, useRef } from "react";
import Editor, { composeDecorators } from "@draft-js-plugins/editor";
import {
  EditorState,
  AtomicBlockUtils,
  convertToRaw,
  RawDraftContentState,
  convertFromRaw,
} from "draft-js";

import { readFile } from "@draft-js-plugins/drag-n-drop-upload";
import createToolbarPlugin, {
  Separator,
} from "@draft-js-plugins/static-toolbar";
import createInlineToolbarPlugin from "@draft-js-plugins/inline-toolbar";
import createUndoPlugin from "@draft-js-plugins/undo";
import "@draft-js-plugins/inline-toolbar/lib/plugin.css";
import "@draft-js-plugins/undo/lib/plugin.css";
import createLinkPlugin from "@draft-js-plugins/anchor";
import {
  ItalicButton,
  BoldButton,
  UnderlineButton,
  HeadlineOneButton,
  HeadlineTwoButton,
  HeadlineThreeButton,
  UnorderedListButton,
  OrderedListButton,
  BlockquoteButton,
  CodeBlockButton,
} from "@draft-js-plugins/buttons";

import "draft-js/dist/Draft.css";
import "@draft-js-plugins/static-toolbar/lib/plugin.css";
import "./editor-overwrite-styles.css";
import { AddImageButton } from "./add-image-btn";
import createImagePlugin from "@draft-js-plugins/image";
import "@draft-js-plugins/image/lib/plugin.css";
import createAlignmentPlugin from "@draft-js-plugins/alignment";
import "@draft-js-plugins/alignment/lib/plugin.css";
import createFocusPlugin from "@draft-js-plugins/focus";
import "@draft-js-plugins/focus/lib/plugin.css";
import createResizeablePlugin from "@draft-js-plugins/resizeable";

import createBlockDndPlugin from "@draft-js-plugins/drag-n-drop";

import createDragNDropUploadPlugin from "@draft-js-plugins/drag-n-drop-upload";
import { errorHandler } from "../../helpers/error-handler";

type CustomEditor = {
  setRawState: (arg: RawDraftContentState) => void;
  content?: string;
};

//upload mocker
type GreetFunction = (
  data: any,
  success: any,
  failed: any,
  progress: any
) => string;
const mockUpload: GreetFunction = (
  data: any,
  success: any,
  failed: any,
  progress: any
) => {
  function doProgress(percent: any) {
    progress(percent || 1);
    if (percent === 100) {
      // Start reading the file
      Promise.all(data.files.map(readFile)).then((files) =>
        success(files, { retainSrc: true })
      );
    } else {
      setTimeout(doProgress, 250, (percent || 0) + 10);
    }
  }

  doProgress(0);
  return "hhhh";
};

//undo/redo
const undoPlugin = createUndoPlugin();
const { UndoButton, RedoButton } = undoPlugin;
//staic toolbar
const staticToolbarPlugin = createToolbarPlugin();
const { Toolbar } = staticToolbarPlugin;
//link plugin
const linkPlugin = createLinkPlugin();
//image plugin
const focusPlugin = createFocusPlugin();
const resizeablePlugin = createResizeablePlugin();
const blockDndPlugin = createBlockDndPlugin();
const alignmentPlugin = createAlignmentPlugin();
const { AlignmentTool } = alignmentPlugin;

const decorator = composeDecorators(
  resizeablePlugin.decorator,
  alignmentPlugin.decorator,
  focusPlugin.decorator,
  blockDndPlugin.decorator
);
const imagePlugin = createImagePlugin({ decorator });

//dragndrop plugin
const dragNDropFileUploadPlugin = createDragNDropUploadPlugin({
  handleUpload: mockUpload,
  // addImage: imagePlugin.addImage,
});

const imgPlugins = [
  dragNDropFileUploadPlugin,
  blockDndPlugin,
  focusPlugin,
  alignmentPlugin,
  resizeablePlugin,
  imagePlugin,
];

function CustomEditor(props: CustomEditor) {
  const { setRawState } = props;
  //setup plugins for editor
  const [plugins, InlineToolbar] = useMemo(() => {
    const inlineToolbarPlugin = createInlineToolbarPlugin();
    return [
      [
        ...imgPlugins,
        inlineToolbarPlugin,
        staticToolbarPlugin,
        linkPlugin,
        undoPlugin,
      ],
      inlineToolbarPlugin.InlineToolbar,
    ];
  }, []);

  //make editor controlled
  const [editorState, setEditorState] = React.useState(() =>
    EditorState.createEmpty()
  );

  const editor = useRef<Editor | null>(null);

  //set the new state after adding the image
  const addImage = (inputValue: string) => {
    if (inputValue.length > 1) {
      console.log(inputValue);
      setEditorState(insertImage(inputValue));
    }
  };
  React.useEffect(() => {
    if (props.content) {
      try {
        const content = convertFromRaw(JSON.parse(props.content));
        const editorStateWithContent = EditorState.createWithContent(content);
        setEditorState(editorStateWithContent);
      } catch (err) {
        errorHandler(err);
      }
    }
  }, []);

  React.useEffect(() => {
    setRawState(convertToRaw(editorState.getCurrentContent()));
  }, [editorState]);
  //insert image in editor state and return the new state
  const insertImage = (url: string) => {
    const contentState = editorState.getCurrentContent();
    const contentStateWithEntity = contentState.createEntity(
      "IMAGE",
      "IMMUTABLE",
      { src: url }
    );
    const entityKey = contentStateWithEntity.getLastCreatedEntityKey();
    const newEditorState = EditorState.set(editorState, {
      currentContent: contentStateWithEntity,
    });
    return AtomicBlockUtils.insertAtomicBlock(newEditorState, entityKey, " ");
  };

  return (
    <>
      <div className="editor">
        <Editor
          editorKey="SimpleInlineToolbarEditor"
          editorState={editorState}
          onChange={setEditorState}
          plugins={plugins}
          ref={(element) => {
            editor.current = element;
          }}
        />
        <AlignmentTool />
        <InlineToolbar>
          {(externalProps: any) => (
            <div>
              <BoldButton {...externalProps} />
              <ItalicButton {...externalProps} />
              <UnderlineButton {...externalProps} />
              <linkPlugin.LinkButton {...externalProps} />
            </div>
          )}
        </InlineToolbar>
        <Toolbar>
          {(externalProps) => (
            <div>
              <HeadlineOneButton {...externalProps} />
              <HeadlineTwoButton {...externalProps} />
              <HeadlineThreeButton {...externalProps} />
              <Separator />
              <UnorderedListButton {...externalProps} />
              <OrderedListButton {...externalProps} />
              <BlockquoteButton {...externalProps} />
              <CodeBlockButton {...externalProps} />
              <Separator />
              <AddImageButton addImage={addImage} />
              <Separator />
              <UndoButton className="toolbar-image-button" />
              <RedoButton className="toolbar-image-button" />
            </div>
          )}
        </Toolbar>
      </div>
    </>
  );
}

export default CustomEditor;
