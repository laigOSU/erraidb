import React, { Component } from 'react';
import ReactDOM from "react-dom";

export class Login extends Component {
  displayName = Login.name
  
render() {
    return (
      <div> {/*JSX ROOT*/}

        
        <div class="card">

            {/*HEADER: LOGIN*/}
            <div class="card-header">
                <h3 class="mb-0">Login</h3>
            </div> {/*End of card-header*/}

            {/*BODY: THE LOGIN FORM*/}    
            <div class="card-body">
                <form action="/">   {/*Hit submit = go to home page*/}
                  <div class="form-group">
                    <label for="email">Email address:</label>
                    <input type="email" class="form-control" id="email"/>
                  </div>
                  <div class="form-group">
                    <label for="pwd">Password:</label>
                    <input type="password" class="form-control" id="pwd"/>
                  </div>
                  <div class="form-group form-check">
                    <label class="form-check-label">
                      <input class="form-check-input" type="checkbox"/> Remember me
                    </label>
                  </div>
                  <button type="submit" class="btn btn-primary">Submit</button>
                </form>
            </div> {/*End of card-body*/}

        </div> {/*End of card*/}
      </div> // End of JSX root in render
    );
  }
}




