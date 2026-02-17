import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { noteService } from '../services/notesService';
import type { NoteType } from '../types/NoteType';

export default function NotePage() {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    const [content, setContent] = useState('');
    const [loading, setLoading] = useState(false);
    const [saving, setSaving] = useState(false);
    const [deleting, setDeleting] = useState(false);

    // load note when id changes
    useEffect(() => {
        if (!id) return;
        setLoading(true);
        noteService
            .getNote(id)
            .then((note) => setContent(note.content))
            .catch((err) => {
                console.error('failed to fetch note', err);
                alert('Unable to load note');
                navigate('/');
            })
            .finally(() => setLoading(false));
    }, [id, navigate]);

    const handleUpdate = async () => {
        if (!id) return;
        setSaving(true);
        try {
            const note: NoteType = { id, content };
            await noteService.saveNote(note);
            alert('Note updated');
        } catch (err) {
            console.error('update failed', err);
            alert('Unable to update note');
        } finally {
            setSaving(false);
        }
    };

    const handleDelete = async () => {
        if (!id) return;
        if (!window.confirm('Are you sure you want to delete this note?')) return;
        setDeleting(true);
        try {
            await noteService.deleteNote(id);
            navigate('/');
        } catch (err) {
            console.error('delete failed', err);
            alert('Unable to delete note');
        } finally {
            setDeleting(false);
        }
    };

    const goBack = () => navigate('/');

    return (
        <div className="card note-card">
            <div className="card-body">
                <h5 className="card-title">Note</h5>

                {loading ? (
                    <p>Loading...</p>
                ) : (
                    <>
                        <div className="mb-3">
                            <label htmlFor="noteContent" className="form-label">
                                Content
                            </label>
                            <textarea
                                className="form-control"
                                id="noteContent"
                                rows={6}
                                value={content}
                                onChange={(e) => setContent(e.target.value)}
                            ></textarea>
                        </div>

                        <button
                            className="btn btn-success me-2"
                            onClick={handleUpdate}
                            disabled={saving || deleting}
                        >
                            {saving ? 'Updating...' : 'Update'}
                        </button>

                        <button
                            className="btn btn-danger me-2"
                            onClick={handleDelete}
                            disabled={saving || deleting}
                        >
                            {deleting ? 'Deleting...' : 'Delete'}
                        </button>

                        <button className="btn btn-secondary" onClick={goBack} disabled={saving || deleting}>
                            Back to menu
                        </button>
                    </>
                )}
            </div>
        </div>
    );
}