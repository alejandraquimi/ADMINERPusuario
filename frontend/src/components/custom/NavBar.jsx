import React, { useState } from "react";

const NavBar = ({ options }) => {
  const [selectedOption, setSelectedOption] = useState(null);

  const handleOptionClick = (option) => {
    setSelectedOption(option);
  };

  return (
    <div>
      <nav
        style={{
          backgroundColor: "green",
          padding: "10px",
          display: "flex",
          justifyContent: "center",
        }}
      >
        {options.map((option) => (
          <div
            key={option.title}
            onClick={() => handleOptionClick(option)}
            style={{
              padding: "10px",
              color: "white",
              fontWeight: "bold",
              cursor: "pointer",
              margin: "0 10px",
            }}
          >
            {option.title}
          </div>
        ))}
      </nav>
      {selectedOption && (
        <div style={{ marginTop: "20px" }}>{selectedOption.children}</div>
      )}
    </div>
  );
};

export default NavBar;
