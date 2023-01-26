import { RawDraftContentState, convertFromRaw } from "draft-js";
import React, { useMemo, useRef } from "react";
import Editor, { composeDecorators } from "@draft-js-plugins/editor";
import { EditorState, AtomicBlockUtils, convertToRaw } from "draft-js";

import "@draft-js-plugins/image/lib/plugin.css";
import createLinkPlugin from "@draft-js-plugins/anchor";

import "draft-js/dist/Draft.css";

import createImagePlugin from "@draft-js-plugins/image";
import "@draft-js-plugins/image/lib/plugin.css";
import createAlignmentPlugin from "@draft-js-plugins/alignment";
import "@draft-js-plugins/alignment/lib/plugin.css";
import createFocusPlugin from "@draft-js-plugins/focus";
import "@draft-js-plugins/focus/lib/plugin.css";
import createResizeablePlugin from "@draft-js-plugins/resizeable";

import createBlockDndPlugin from "@draft-js-plugins/drag-n-drop";

import "./editor-overwrite-styles.css";
import { errorHandler } from "../../helpers/error-handler";
import { enqueueSnackbar } from "notistack";

type ReadonlyEditor = {
  content: string | undefined;
};

//link plugin
const linkPlugin = createLinkPlugin();
//image plugin
const focusPlugin = createFocusPlugin();
const resizeablePlugin = createResizeablePlugin();
const blockDndPlugin = createBlockDndPlugin();
const alignmentPlugin = createAlignmentPlugin();
const { AlignmentTool } = alignmentPlugin;

const decorator = composeDecorators(resizeablePlugin.decorator);
const imagePlugin = createImagePlugin({ decorator });

const plugins = [
  alignmentPlugin,
  focusPlugin,
  resizeablePlugin,
  imagePlugin,
  linkPlugin,
];
function ReadonlyEditor(props: ReadonlyEditor) {
  //setup plugins for editor

  //make editor controlled
  const [editorState, setEditorState] = React.useState(() =>
    EditorState.createEmpty()
  );

  const editor = useRef<Editor | null>(null);

  //populate read only editor with content
  React.useEffect(() => {
    if (props.content != null) {
      try {
        const content = convertFromRaw(JSON.parse(props.content));
        const editorStateWithContent = EditorState.createWithContent(content);
        setEditorState(editorStateWithContent);
      } catch (err) {
        errorHandler(err);
      }
    }
  }, []);

  return (
    <>
      <div className="readonly-editor">
        <Editor
          readOnly={true}
          editorKey="SimpleInlineToolbarEditor"
          editorState={editorState}
          onChange={setEditorState}
          plugins={[...plugins]}
          ref={(element) => {
            editor.current = element;
          }}
        />
        <AlignmentTool />
      </div>
    </>
  );
}

export default ReadonlyEditor;
