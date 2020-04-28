import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { registration } from '../../actions/regitstration'
import { Link, Redirect } from 'react-router-dom';
import { store } from '../..';
import { accounts } from '../../reducers/accounts';

class RegistrationForm extends React.Component<any, any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            username: "",
            password: "",
            email: ""
        }
    }

    render() {
        const { error, isLoading, accessToken } = this.props.accounts;
        if (accessToken != '') {
            return (
            <>
                <Redirect to="account"/>
            </>
            )
        }
        if (error != null) {
            return (
            <div>
                <p>{error}</p>
                <Link to="/signIn"></Link>
            </div>
            );
        }
        else {
            return (
                <div>
                    <form onSubmit={this.handleSubmit.bind(this)}>
                        <input type="text"
                            placeholder="login"
                            onChange={this.handleLoginChange.bind(this)}
                        />
                        <br />
                        <input type="email"
                            placeholder="email"
                            onChange={this.handleEmailChange.bind(this)}
                        />
                        <br />
                        <input type="password"
                            placeholder="password"
                            onChange={this.handlePasswordChange.bind(this)}
                        />
                        <br />
                        <button>Регестрация</button>

                    </form>

                </div>
            );
        }
    }

    handleLoginChange(event: any) {
        this.setState({
            ...this.state,
            username: event.target.value
        });
    }

    handlePasswordChange(event: any) {
        this.setState({
            ...this.state,
            password: event.target.value,
        });
    }

    handleEmailChange(event: any) {
        this.setState({
            ...this.state,
            email: event.target.value
        });
    }

    handleSubmit(event: any) {
        event.preventDefault();
        const { username, email, password } = this.state;
        this.props.registration({
            username,
            email,
            password
        })
    }
}

const mapDispatchToProps = (dispatch: any) => bindActionCreators({ registration }, dispatch)
const mapStateToProps = (state: any) => ({ ...state });

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationForm);
