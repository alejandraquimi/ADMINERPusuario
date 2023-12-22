import React, { useState, useEffect } from "react";
import Snackbar from "@mui/material/Snackbar";
import MuiAlert from "@mui/material/Alert";
import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";

const CustomSnackbar = ({ snackbarMessage, onClose }) => {
  const [open, setOpen] = useState(false);

  useEffect(() => {
    if (snackbarMessage) {
      setOpen(true);
    }
  }, [snackbarMessage]);

  const handleClose = () => {
    setOpen(false);
    onClose(); // Llama a la función onClose para que el padre pueda manejar la cola de mensajes
  };

  return (
    <>
      {open && snackbarMessage && (
        <Snackbar
          open={open}
          autoHideDuration={6000}
          onClose={handleClose}
          anchorOrigin={{
            vertical: "top",
            horizontal: "right",
          }}
        >
          <MuiAlert
            elevation={6}
            variant="filled"
            severity={snackbarMessage.severity}
            action={
              <IconButton
                className="icon-button-snackbar"
                size="small"
                aria-label="close"
                color="inherit"
                onClick={handleClose}
              >
                <CloseIcon fontSize="small" />
              </IconButton>
            }
          >
            {snackbarMessage.message}
          </MuiAlert>
        </Snackbar>
      )}
    </>
  );
};

export default CustomSnackbar;
