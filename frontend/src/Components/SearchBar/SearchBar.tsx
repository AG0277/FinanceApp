import React, { ChangeEvent, useState, SyntheticEvent, FormEvent } from "react";

interface Props {
  onSearchSubmit: (e: SyntheticEvent) => void;
  search: string | undefined;
  handleSearchChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

const SearchBar: React.FC<Props> = ({
  onSearchSubmit,
  search,
  handleSearchChange,
}: Props): JSX.Element => {
  return (
    <>
      <form onSubmit={onSearchSubmit} action="">
        <input value={search} onChange={handleSearchChange} />
      </form>
    </>
  );
};

export default SearchBar;
