import { Link } from "react-router-dom"
import "./menu.scss"
import { menu } from "../../data";
import { AppStore } from "../../redux/store";
import { useSelector } from "react-redux";
import { AccountInfo } from "../../models";
import React from "react";

export const Menu = () => {

  const accountInfoState: AccountInfo = useSelector((store: AppStore) => store.accountInfo);

  return (
    <div className="menu">
      {menu.map((item) => (
        <div className="item" key={'parent-div' + item.id}>
          <span key={'parent-span' + item.id} className="title">{item.title}</span>
          {item.listItems.map((listItem) => (
            <React.Fragment key={item.id + '-' + listItem.id}>
              {
                accountInfoState.user.roles.some(role => listItem.roles.includes(role.id)) ? (
                  <>
                    <Link to={listItem.url} className="listItem" key={listItem.id}>
                      <img key={'img' + listItem.id} src={listItem.icon} alt={listItem.id + "_" + listItem.title + " icon"} />
                      <span key={'span' + listItem.id} className="listItemTitle">{listItem.title}</span>
                    </Link>
                  </>
                ) : (
                  <React.Fragment key={listItem.id}/>
                )
              }
            </React.Fragment>
          ))}
        </div>
      ))}
    </div>
  )
}

export default Menu