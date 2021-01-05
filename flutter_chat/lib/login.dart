import 'package:flutter/material.dart';
import 'sign.dart';
import 'chat.dart';
import 'package:flutter_chat/shared/entry_field.dart';

class LoginPage extends StatefulWidget {
  LoginPage({Key key, this.title}) : super(key: key);
  final String title;

  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();

  Widget _submitButton() {
    return TextButton(
      child: Text('Login'),
      onPressed: () => {
        Navigator.push(
            context, MaterialPageRoute(builder: (context) => ChatScreen()))
      },
    );
  }

  Widget _createAccountLabel() {
    return TextButton(
      child: Text('Register'),
      onPressed: () => {
        Navigator.push(
            context, MaterialPageRoute(builder: (context) => SignUpPage()))
      },
    );
  }

  Widget _title() {
    return RichText(
      textAlign: TextAlign.center,
      text: TextSpan(text: 'Y.A.P.C.'),
    );
  }

  Widget _emailPasswordWidget() {
    return Column(
      children: <Widget>[
        EntryField(
          controller: _usernameController,
          textDecoration: 'Username',
          isPassword: false,
        ),
        EntryField(
            controller: _passwordController,
            textDecoration: 'Password',
            isPassword: true),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    final height = MediaQuery.of(context).size.height;
    return Scaffold(
        body: Container(
      height: height,
      child: Stack(
        children: <Widget>[
          Positioned(
              top: -height * .15,
              right: -MediaQuery.of(context).size.width * .4,
              child: Container()),
          Container(
            padding: EdgeInsets.symmetric(horizontal: 20),
            child: SingleChildScrollView(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  SizedBox(height: height * .2),
                  _title(),
                  SizedBox(height: 50),
                  _emailPasswordWidget(),
                  SizedBox(height: 20),
                  _submitButton(),
                  SizedBox(height: height * .055),
                  _createAccountLabel(),
                ],
              ),
            ),
          )
        ],
      ),
    ));
  }
}
