import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { noteService } from "../services/notesService";

export default function EmptyPage() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);

    const createNote = async () => {
        setLoading(true);
        try {
            const id = await noteService.createNote();
            navigate(`/${id}`);
        } catch (err) {
            console.error("failed to create note", err);
            alert("Unable to create note. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="card">
            <div className="card-body">
                <h5 className="card-title">No Note Selected</h5>
                <p className="card-text">
                    Welcome to Shared Notepad!<br />
                    Enter a note ID in the address bar to view or create one, e.g. <code className="text-muted">/12345</code>
                </p>
                <button
                    className="btn btn-primary"
                    onClick={createNote}
                    disabled={loading}
                >
                    {loading ? (
                        <>
                            <span
                                className="spinner-border spinner-border-sm me-2"
                                role="status"
                                aria-hidden="true"
                            ></span>
                            Creating...
                        </>
                    ) : (
                        "Create New Note"
                    )}
                </button>
            </div>
        </div>
    );
}