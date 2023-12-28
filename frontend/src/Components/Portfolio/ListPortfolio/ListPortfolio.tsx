import React, { SyntheticEvent } from "react";
import CardPortfolio from "../CardPortfolio/CardPortfolio";

interface Props {
  portfolioValues: string[];
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

const ListPortfolio = ({ portfolioValues, onPortfolioDelete }: Props) => {
  return (
    <>
      <h3>MyPortfolio</h3>
      <ul>
        {portfolioValues &&
          portfolioValues.map((portfolioValue, index) => {
            return (
              <CardPortfolio
                key={index}
                portfolioValue={portfolioValue}
                onPortfolioDelete={onPortfolioDelete}
              />
            );
          })}
      </ul>
    </>
  );
};

export default ListPortfolio;
