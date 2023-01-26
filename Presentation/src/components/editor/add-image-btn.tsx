import {
  Box,
  Button,
  Menu,
  MenuItem,
  TextField,
  Typography,
} from "@mui/material";
import React from "react";
import { ReactComponent as ImageIcon } from "./img.svg";
type AddImageButtonProps = {
  addImage: (inputValue: string) => void;
};
export const AddImageButton = ({ addImage }: AddImageButtonProps) => {
  //handle menu popup
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  //cntroll link textfield
  const [textFieldValue, setTextFieldValue] = React.useState<string>("");

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setTextFieldValue(event.target.value);
  };
  const handleAdding = () => {
    addImage(textFieldValue);
    handleClose();
  };
  return (
    <div className="toolbar-button-container">
      <button
        className="toolbar-image-button"
        onClick={handleClick}
        type="button"
      >
        <ImageIcon className="toolbar-image-icon" />
      </button>
      <Menu
        id="basic-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        MenuListProps={{
          "aria-labelledby": "basic-button",
        }}
      >
        <Box sx={{ padding: "4px 8px" }}>
          <TextField
            label="Image URL"
            size="small"
            variant="outlined"
            value={textFieldValue}
            onChange={handleChange}
          />
          <Button onClick={handleAdding} variant="contained" type="button">
            Add
          </Button>
        </Box>
      </Menu>
    </div>
  );
};
