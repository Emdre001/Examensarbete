import React from 'react';
import './styles/ToastNotification.css';

const ToastNotification = ({ show, message }) => (
  <div className={`toast-notification${show ? ' show' : ''}`}>
    {message}
  </div>
);

export default ToastNotification;