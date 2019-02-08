import React, { Component } from 'react';
import ReactDOM from "react-dom";


export class SignUp extends Component {
  displayName = SignUp.name
  
render() {
    return (

      <div> {/*JSX ROOT*/}
        
        <div class="card">
            <article class="card-body">
                <h4 class="card-title text-center mb-4 mt-1">SIGN UP</h4>

                <p class="text-success text-center">Please create an account</p>
                <form action="/">



                <div class="form-group">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"> <i class="fa fa-user"></i> </span>
                     </div>
                    <input class="form-control" placeholder="First Name" type="text" value=""/>
                </div> 
                </div> 



                <div class="form-group">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"> <i class="fa fa-user"></i> </span>
                     </div>
                    <input class="form-control" placeholder="Last Name" type="text" value=""/>
                </div> 
                </div> 



                <div class="form-group">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"> <i class="fa fa-envelope"></i> </span>
                     </div>
                    <input name="" class="form-control" placeholder="Email" type="email" value=""/>
                </div> 
                </div> 



                <div class="form-group">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"> <i class="fa fa-lock"></i> </span>
                     </div>
                    <input class="form-control" placeholder="******" type="password" value=""/>
                </div> 
                </div> 


                <div class="row">
                    <div class = "col-md-6">

                        <div class="form-group">
                        <button type="submit" class="btn btn-primary btn-block"> Create My Account</button>
                        </div>
                    </div>

                </div>


                </form>
            </article>

            </div> 
      </div> // End of JSX root in render
    );
  }
}




