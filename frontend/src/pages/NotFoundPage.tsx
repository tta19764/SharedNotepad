import { Link } from 'react-router-dom';

export default function NotFoundPage() {
    return (
        <div className="card text-center">
            <div className="card-body">
                <h5 className="card-title">404 - Page not found</h5>
                <p className="card-text">
                    The note you're looking for doesn't exist or the URL is incorrect.
                </p>
                <Link to="/" className="btn btn-primary">
                    Go back home
                </Link>
            </div>
        </div>
    );
}